import bpy, sys, json, os, traceback

def argv():
    return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []

def fail(msg, code=2):
    print('[ShellStudio ERROR]', msg, file=sys.stderr)
    raise SystemExit(code)

def addon_loaded():
    return any('sollumz' in str(m).lower() for m in dir(bpy.ops)) or 'Sollumz' in bpy.context.preferences.addons.keys() or 'sollumz' in bpy.context.preferences.addons.keys()

def box_mesh(name, pos, scale):
    bpy.ops.mesh.primitive_cube_add(location=(pos['X'],pos['Y'],pos['Z']))
    o=bpy.context.object; o.name=name
    o.scale=(scale['X']/2,scale['Y']/2,scale['Z']/2)
    bpy.ops.object.transform_apply(location=False,rotation=False,scale=True)
    return o

def build(project):
    bpy.ops.object.select_all(action='SELECT'); bpy.ops.object.delete(use_global=False)
    for item in project.get('Objects',[]):
        o=box_mesh(item.get('Name','Object'),item.get('Position',{'X':0,'Y':0,'Z':0}),item.get('Scale',{'X':1,'Y':1,'Z':1}))
        r=item.get('Rotation',{'X':0,'Y':0,'Z':0}); o.rotation_euler=[v*0.017453292519943295 for v in (r['X'],r['Y'],r['Z'])]
        o['tc_type']=item.get('Type',0);o['tc_collision']=item.get('Collision',True);o['tc_floor']=item.get('FloorLevel',0)

def main():
    a=argv()
    if len(a)<2: fail('Usage: shellstudio_export.py project.tcshell output_dir')
    project_path=os.path.abspath(a[0]);out=os.path.abspath(a[1]);os.makedirs(out,exist_ok=True)
    if not os.path.isfile(project_path): fail('Project payload missing: '+project_path)
    with open(project_path,'r',encoding='utf-8') as f: project=json.load(f)
    build(project)
    blend=os.path.join(out,'shellstudio_generated.blend');bpy.ops.wm.save_as_mainfile(filepath=blend)
    status={'blender':bpy.app.version_string,'sollumz_loaded':addon_loaded(),'objects':len(bpy.context.scene.objects),'blend':blend}
    with open(os.path.join(out,'bridge-status.json'),'w',encoding='utf-8') as f: json.dump(status,f,indent=2)
    if not status['sollumz_loaded']: fail('Sollumz is not enabled in this Blender profile; GTA export was not attempted.',3)
    print('[ShellStudio] Scene generated and Sollumz detected. GTA operator mapping is required for final asset export.')

if __name__=='__main__':
    try: main()
    except SystemExit: raise
    except Exception: traceback.print_exc(); raise SystemExit(10)
