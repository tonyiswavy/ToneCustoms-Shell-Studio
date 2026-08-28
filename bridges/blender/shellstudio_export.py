import bpy, sys, json, os, traceback, math
TARGET_BLENDER=(4,5); TARGET_SOLLUMZ='2.9.0'; EXPORT_OPERATOR='sollumz.export_assets'
def argv(): return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []
def fail(msg,code=2): print('[ShellStudio ERROR]',msg,file=sys.stderr);raise SystemExit(code)
def sollumz_loaded(): return any('sollumz' in k.lower() for k in bpy.context.preferences.addons.keys())
def sollumz_version():
    for key in bpy.context.preferences.addons.keys():
        if 'sollumz' not in key.lower(): continue
        try:
            mod=sys.modules.get(key) or __import__(key);v=(getattr(mod,'bl_info',{}) or {}).get('version')
            if v:return '.'.join(map(str,v[:3]))
        except Exception:pass
    return None
def operator_inventory():
    names=[]
    for group in ('sollumz','sollumz_export','export_scene'):
        ns=getattr(bpy.ops,group,None)
        if ns:
            try:names.extend(f'{group}.{n}' for n in dir(ns) if not n.startswith('_'))
            except Exception:pass
    return sorted(set(names))
def cube(name,pos,scale,rot):
    bpy.ops.mesh.primitive_cube_add(location=(pos['X'],pos['Y'],pos['Z']))
    o=bpy.context.object;o.name=name;o.scale=(scale['X']/2,scale['Y']/2,scale['Z']/2);o.rotation_euler=tuple(math.radians(rot[k]) for k in ('X','Y','Z'));bpy.ops.object.transform_apply(location=False,rotation=False,scale=True);return o
def build(project):
    bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False);built=[]
    for item in project.get('Objects',[]):
        o=cube(item.get('Name','Object'),item.get('Position',{'X':0,'Y':0,'Z':0}),item.get('Scale',{'X':1,'Y':1,'Z':1}),item.get('Rotation',{'X':0,'Y':0,'Z':0}));o['tc_type']=item.get('Type',0);o['tc_collision']=item.get('Collision',True);o['tc_floor']=item.get('FloorLevel',0);o['tc_model']=item.get('ModelName') or '';built.append(o)
    return built
def select_only(objects):
    bpy.ops.object.select_all(action='DESELECT')
    for o in objects:o.select_set(True)
    if objects:bpy.context.view_layer.objects.active=objects[0]
def invoke_first(candidates,**kwargs):
    for full in candidates:
        group,name=full.split('.',1);ns=getattr(bpy.ops,group,None);op=getattr(ns,name,None) if ns else None
        if op is None:continue
        try:
            result=op(**kwargs)
            if 'FINISHED' in result:return full
        except Exception as ex:print('[ShellStudio]',full,'failed:',ex,file=sys.stderr)
    return None
def convert_to_sollumz(project,meshes):
    if not meshes:fail('Shell contains no geometry to convert.',11)
    select_only(meshes)
    # Sollumz 2.9 conversion operators have changed names over time; resolve only registered operators.
    drawable_candidates=['sollumz.convert_to_drawable','sollumz.create_drawable','sollumz.create_drawable_obj','sollumz.create_drawable_objects']
    used_drawable=invoke_first(drawable_candidates)
    if not used_drawable:fail('Sollumz is loaded but no registered Convert to Drawable operator matched this 2.9.0 installation.',12)
    collision_meshes=[o for o in meshes if bool(o.get('tc_collision',True)) and o.name in bpy.data.objects]
    used_collision=None
    if collision_meshes:
        select_only(collision_meshes)
        collision_candidates=['sollumz.convert_to_composite','sollumz.create_bound_composite','sollumz.create_bounds']
        used_collision=invoke_first(collision_candidates)
        if not used_collision:fail('Collision is enabled but no registered Sollumz Create Bounds/Composite operator matched this installation.',13)
    roots=[o for o in bpy.context.scene.objects if getattr(o,'sollum_type',None) and str(getattr(o,'sollum_type','')).upper() in ('DRAWABLE','BOUND_COMPOSITE')]
    if not roots:fail('Sollumz conversion completed without producing a Drawable/Bound root.',14)
    select_only(roots)
    return used_drawable,used_collision,[o.name for o in roots]
def export_assets(out):
    op=getattr(getattr(bpy.ops,'sollumz',None),'export_assets',None)
    if op is None:fail(f'Required operator {EXPORT_OPERATOR} is unavailable.',7)
    before=set(os.listdir(out));result=op(directory=out,direct_export=True);after=set(os.listdir(out));created=sorted(after-before)
    if 'FINISHED' not in result:fail(f'Sollumz export returned {result}.',8)
    gta=[x for x in created if os.path.splitext(x)[1].lower() in ('.ydr','.ydd','.ybn','.ytd','.ymap','.ytyp','.yft')];xml=[x for x in created if x.lower().endswith('.xml')]
    return gta,xml,created
def write_status(out,status):
    with open(os.path.join(out,'bridge-status.json'),'w',encoding='utf-8') as f:json.dump(status,f,indent=2)
def main():
    a=argv()
    if len(a)<2:fail('Usage: project output')
    project_path=os.path.abspath(a[0]);out=os.path.abspath(a[1]);os.makedirs(out,exist_ok=True)
    with open(project_path,'r',encoding='utf-8') as f:project=json.load(f)
    meshes=build(project);sv=sollumz_version();ops=operator_inventory();compatible=bpy.app.version[:2]==TARGET_BLENDER and (sv is None or sv==TARGET_SOLLUMZ)
    status={'blender':bpy.app.version_string,'target_blender':'4.5','sollumz_loaded':sollumz_loaded(),'sollumz_version':sv,'target_sollumz':TARGET_SOLLUMZ,'toolchain_compatible':compatible,'sollumz_operators':ops,'selected_export_operator':EXPORT_OPERATOR,'objects':len(meshes),'gta_export_completed':False,'gta_files':[],'xml_files':[]};write_status(out,status)
    if not status['sollumz_loaded']:fail('Sollumz is not enabled; GTA export stopped.',3)
    if not compatible:fail(f'Unsupported toolchain. Expected Blender 4.5 + Sollumz {TARGET_SOLLUMZ}; detected Blender {bpy.app.version_string} + Sollumz {sv or "unknown"}.',4)
    used_drawable,used_collision,roots=convert_to_sollumz(project,meshes);status['drawable_converter']=used_drawable;status['collision_converter']=used_collision;status['asset_roots']=roots
    blend=os.path.join(out,'shellstudio_generated.blend');bpy.ops.wm.save_as_mainfile(filepath=blend);status['blend']=blend;write_status(out,status)
    if EXPORT_OPERATOR not in ops:fail(f'{EXPORT_OPERATOR} was not registered by the installed Sollumz build.',5)
    gta,xml,created=export_assets(out);status['gta_files']=gta;status['xml_files']=xml;status['created_files']=created;status['gta_export_completed']=len(gta)>0;write_status(out,status)
    if not gta:fail('Sollumz conversion/export ran, but no native GTA files were produced.',9)
    print('[ShellStudio]',f'GTA export complete: {len(gta)} native file(s)')
if __name__=='__main__':
    try:main()
    except SystemExit:raise
    except Exception:traceback.print_exc();raise SystemExit(10)
