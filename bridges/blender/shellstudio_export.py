import bpy, sys, json, os, traceback, math

def argv(): return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []
def fail(msg,code=2): print('[ShellStudio ERROR]',msg,file=sys.stderr);raise SystemExit(code)
def sollumz_loaded(): return any('sollumz' in k.lower() for k in bpy.context.preferences.addons.keys())
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
    status={'blender':bpy.app.version_string,'sollumz_loaded':sollumz_loaded(),'objects':len(bpy.context.scene.objects),'blend':blend,'gta_export_completed':False}
    with open(os.path.join(out,'bridge-status.json'),'w',encoding='utf-8') as f:json.dump(status,f,indent=2)
    if not status['sollumz_loaded']: fail('Sollumz is not enabled; GTA export stopped.',3)
    # Exact Sollumz export operator varies by installed Sollumz version. Version-specific adapter must set gta_export_completed=true only after real output is written.
    fail('Sollumz detected, but no verified version-specific GTA export adapter is installed yet.',4)
if __name__=='__main__':
    try:main()
    except SystemExit:raise
    except Exception:traceback.print_exc();raise SystemExit(10)
