using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class CameraController { public double Yaw {get;private set;}=.65;public double Pitch {get;private set;}=.55;public double Zoom {get;private set;}=55;public Vec3 Target {get;private set;}=new();public void Orbit(double dx,double dy){Yaw+=dx*.01;Pitch=Math.Clamp(Pitch+dy*.01,-1.4,1.4);}public void Dolly(double delta)=>Zoom=Math.Clamp(Zoom+delta,10,250);public void Pan(float x,float y,float z=0)=>Target=new(Target.X+x,Target.Y+y,Target.Z+z); }
