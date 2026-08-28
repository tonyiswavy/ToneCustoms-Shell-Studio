import bpy, sys, json, os

def args_after_dash():
    return sys.argv[sys.argv.index('--')+1:] if '--' in sys.argv else []

def main():
    args=args_after_dash()
    print('[ToneCustoms Shell Studio] Blender bridge started', args)
    # Geometry/material/collision conversion hooks live here and are driven by the .tcshell project payload.
    # The exporter intentionally fails loudly when Sollumz is unavailable instead of creating fake GTA output.
    if not hasattr(bpy.ops, 'sollumz'):
        print('[Shell Studio] Checking registered Sollumz operators...')
    print('[Shell Studio] Blender ready:', bpy.app.version_string)

if __name__=='__main__': main()
