import bpy, sys, json, os, traceback, math
TARGET_BLENDER=(4,5); TARGET_SOLLUMZ='2.9.0'; EXPORT_OPERATOR='sollumz.export_assets'
SZ_DRAWABLE='sollumz_drawable'; SZ_DRAWABLE_MODEL='sollumz_drawable_model'; SZ_DRAWABLE_GEOMETRY='sollumz_drawable_geometry'; SZ_BOUND_COMPOSITE='sollumz_bound_composite'; SZ_BOUND_BOX='sollumz_bound_box'
def argv(): return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []
def fail(msg,code=2): print('[ShellStudio ERROR]',msg,file=sys.stderr);raise SystemExit(code)
def sollumz_loaded(): return any('sollumz' in k.lower() for k in bpy.context.preferences.addons.keys())
def sollumz_version():
    for key in bpy.context.preferences.addons.keys():
        if 'sollumz' not in key.lower(): continue
        try:
            mod=sys.modules.get(key) or __import__(key); info=getattr(mod,'bl_info',{}) or {}; v=info.get('version')
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
def empty(name,sz_type):
    o=bpy.data.objects.new(name,None);bpy.context.collection.objects.link(o);o.sollum_type=sz_type;return o
def build(project):
    bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
    visual=[];collision=[]
    for item in project.get('Objects',[]):
        o=cube(item.get('Name','Object'),item.get('Position',{'X':0,'Y':0,'Z':0}),item.get('Scale',{'X':1,'Y':1,'Z':1}),item.get('Rotation',{'X':0,'Y':0,'Z':0}));o['tc_type']=item.get('Type',0);o['tc_collision']=item.get('Collision',True);o['tc_floor']=item.get('FloorLevel',0);o['tc_model']=item.get('ModelName') or '';visual.append(o)
        if item.get('Collision',True):
            c=o.copy();c.data=o.data.copy();bpy.context.collection.objects.link(c);c.name=o.name+'_collision';c.sollum_type=SZ_BOUND_BOX;collision.append(c)
    return visual,collision
def convert_to_sollumz(project,visual,collision):
    name=(project.get('Name') or 'tc_shell').strip().lower().replace(' ','_')
    drawable=empty(name,SZ_DRAWABLE);model=empty(name+'_model',SZ_DRAWABLE_MODEL);model.parent=drawable
    for o in visual:o.parent=model;o.sollum_type=SZ_DRAWABLE_GEOMETRY
    bound=None
    if collision:
        bound=empty(name+'_collision',SZ_BOUND_COMPOSITE)
        for c in collision:c.parent=bound
    bpy.ops.object.select_all(action='DESELECT');drawable.select_set(True);bpy.context.view_layer.objects.active=drawable
    if bound:bound.select_set(True)
    return drawable,bound
def export_assets(out):
    op=getattr(getattr(bpy.ops,'sollumz',None),'export_assets',None)
    if op is None: fail(f'Required operator {EXPORT_OPERATOR} is unavailable.',7)
    before=set(os.listdir(out));result=op(directory=out,direct_export=True,use_custom_settings=True,formats={'NATIVE'},target_versions={'GEN8'},limit_to_selected=True);after=set(os.listdir(out));created=sorted(after-before)
    if 'FINISHED' not in result: fail(f'Sollumz export returned {result}.',8)
    gta=[];xml=[]
    for root,_,files in os.walk(out):
        for f in files:
            rel=os.path.relpath(os.path.join(root,f),out);ext=os.path.splitext(f)[1].lower()
            if ext in ('.ydr','.ydd','.ybn','.ytd','.ymap','.ytyp','.yft'):gta.append(rel)
            elif f.lower().endswith('.xml'):xml.append(rel)
    return sorted(set(gta)),sorted(set(xml)),created
def write_status(out,status):
    with open(os.path.join(out,'bridge-status.json'),'w',encoding='utf-8') as f:json.dump(status,f,indent=2)
def main():
    a=argv()
    if len(a)<2: fail('Usage: project output')
    project_path=os.path.abspath(a[0]);out=os.path.abspath(a[1]);os.makedirs(out,exist_ok=True)
    with open(project_path,'r',encoding='utf-8') as f: project=json.load(f)
    visual,collision=build(project);drawable,bound=convert_to_sollumz(project,visual,collision);blend=os.path.join(out,'shellstudio_generated.blend');bpy.ops.wm.save_as_mainfile(filepath=blend)
    sv=sollumz_version();ops=operator_inventory();compatible=bpy.app.version[:2]==TARGET_BLENDER and (sv is None or sv==TARGET_SOLLUMZ)
    status={'blender':bpy.app.version_string,'target_blender':'4.5','sollumz_loaded':sollumz_loaded(),'sollumz_version':sv,'target_sollumz':TARGET_SOLLUMZ,'toolchain_compatible':compatible,'sollumz_operators':ops,'selected_export_operator':EXPORT_OPERATOR,'objects':len(bpy.context.scene.objects),'visual_objects':len(visual),'collision_objects':len(collision),'drawable':drawable.name,'bound':bound.name if bound else None,'blend':blend,'gta_export_completed':False,'gta_files':[],'xml_files':[]}
    write_status(out,status)
    if not status['sollumz_loaded']: fail('Sollumz is not enabled; GTA export stopped.',3)
    if not compatible: fail(f'Unsupported toolchain. Expected Blender 4.5 + Sollumz {TARGET_SOLLUMZ}; detected Blender {bpy.app.version_string} + Sollumz {sv or "unknown"}.',4)
    if EXPORT_OPERATOR not in ops: fail(f'{EXPORT_OPERATOR} was not registered by the installed Sollumz build.',5)
    gta,xml,created=export_assets(out);status['gta_files']=gta;status['xml_files']=xml;status['created_files']=created;status['gta_export_completed']=len(gta)>0;write_status(out,status)
    if not gta: fail('Sollumz export ran, but no native GTA files were produced. Check bridge-status.json and Blender console output.',9)
    print('[ShellStudio]',f'GTA export complete: {len(gta)} native file(s)')
if __name__=='__main__':
    try:main()
    except SystemExit:raise
    except Exception:traceback.print_exc();raise SystemExit(10)
