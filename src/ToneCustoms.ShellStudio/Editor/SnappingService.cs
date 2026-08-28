using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class SnappingService { public Vec3 Grid(Vec3 v,float g)=>new(MathF.Round(v.X/g)*g,MathF.Round(v.Y/g)*g,MathF.Round(v.Z/g)*g);public Vec3 Smart(Vec3 p,IEnumerable<SceneObject> objects,float t=.2f){foreach(var o in objects){var dx=p.X-o.Position.X;var dy=p.Y-o.Position.Y;var dz=p.Z-o.Position.Z;if(MathF.Sqrt(dx*dx+dy*dy+dz*dz)<=t)return new(o.Position.X,o.Position.Y,o.Position.Z);}return p;} }
