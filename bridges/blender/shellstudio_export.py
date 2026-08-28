import bpy, sys, json, os, traceback, math
TARGET_BLENDER=(4,5); TARGET_SOLLUMZ='2.9.0'
def argv(): return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []
def fail(msg,code=2): print('[ShellStudio ERROR]',msg,file=sys.stderr);raise SystemExit(code)
def sollumz_loaded(): return any('sollumz' in k.lower() for k in bpy.context.preferences.addons.keys())
def sollumz_version():
    for key,addon in bpy.context.preferences.addons.items():
        if 'sollumz' not in key.lower(): continue
        try:
            mod=sys.modules.get(key) or __import__(key); info=getattr(mod,'bl_info',{}) or {}; v=info.get('version');
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
    bpy.ops.object.select_all(action='SELECT');bpy.ops.object.delete(use_global=False)
    for item in project.get('Objects',[]):
        o=cube(item.get('Name','Object'),item.get('Position',{'X':0,'Y':0,'Z':0}),item.get('Scale',{'X':1,'Y':1,'Z':1}),item.get('Rotation',{'X':0,'Y':0,'Z':0}));o['tc_type']=item.get('Type',0);o['tc_collision']=item.get('Collision',True);o['tc_floor']=item.get('FloorLevel',0);o['tc_model']=item.get('ModelName') or ''
def main():
    a=argv()
    if len(a)<2: fail('Usage: project output')
    project_path=os.path.abspath(a[0]);out=os.path.abspath(a[1]);os.makedirs(out,exist_ok=True)
    with open(project_path,'r',encoding='utf-8') as f: project=json.load(f)
    build(project);blend=os.path.join(out,'shellstudio_generated.blend');bpy.ops.wm.save_as_mainfile(filepath=blend)
    sv=sollumz_version(); ops=operator_inventory(); compatible=bpy.app.version[:2]==TARGET_BLENDER and (sv is None or sv==TARGET_SOLLUMZ)
    status={'blender':bpy.app.version_string,'target_blender':'4.5','sollumz_loaded':sollumz_loaded(),'sollumz_version':sv,'target_sollumz':TARGET_SOLLUMZ,'toolchain_compatible':compatible,'sollumz_operators':ops,'objects':len(bpy.context.scene.objects),'blend':blend,'gta_export_completed':False}
    with open(os.path.join(out,'bridge-status.json'),'w',encoding='utf-8') as f:json.dump(status,f,indent=2)
    if not status['sollumz_loaded']: fail('Sollumz is not enabled; GTA export stopped.',3)
    if not compatible: fail(f'Unsupported toolchain. Expected Blender 4.5 + Sollumz {TARGET_SOLLUMZ}; detected Blender {bpy.app.version_string} + Sollumz {sv or "unknown"}.',4)
    # Probe the installed 2.9.0 operator surface first. We never guess/call an export operator until the exact installed API is confirmed.
    if not ops: fail('Sollumz 2.9.0 detected but no callable export operators were discovered. See bridge-status.json.',5)
    fail('Sollumz 2.9.0 capability probe complete. Exact export operator must be verified from bridge-status.json before enabling GTA writes.',6)
if __name__=='__main__':
    try:main()
    except SystemExit:raise
    except Exception:traceback.print_exc();raise SystemExit(10)
