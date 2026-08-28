using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Preview;
public sealed class WalkPreview { public Vec3 Position {get;private set;}=new(0,0,1.8f);public float EyeHeight {get;set;}=1.65f;public void Spawn(Vec3 p)=>Position=new(p.X,p.Y,p.Z+EyeHeight);public void Move(Vec3 d,ShellProject project)=>Position=new(Position.X+d.X,Position.Y+d.Y,Position.Z+d.Z); }
