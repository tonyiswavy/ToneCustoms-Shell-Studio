using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Props;
namespace ToneCustoms.ShellStudio.Editor;
public sealed record Measurement(float Distance,Vec3 Delta);
public sealed class StudioBuildTools
{
 public Vec3 SmartSnap(ShellProject project,SceneObject moving,Vec3 candidate,float tolerance=.2f){var result=candidate;foreach(var o in project.Objects.Where(x=>x.Id!=moving.Id&&x.FloorLevel==moving.FloorLevel)){foreach(var x in new[]{o.Position.X-o.Scale.X/2,o.Position.X+o.Scale.X/2,o.Position.X})if(Math.Abs(result.X-x)<=tolerance)result=new Vec3(x,result.Y,result.Z);foreach(var y in new[]{o.Position.Y-o.Scale.Y/2,o.Position.Y+o.Scale.Y/2,o.Position.Y})if(Math.Abs(result.Y-y)<=tolerance)result=new Vec3(result.X,y,result.Z);if(Math.Abs(result.Z-o.Position.Z)<=tolerance)result=new Vec3(result.X,result.Y,o.Position.Z);}return result;}
 public Measurement Measure(Vec3 a,Vec3 b){var d=new Vec3(b.X-a.X,b.Y-a.Y,b.Z-a.Z);return new(MathF.Sqrt(d.X*d.X+d.Y*d.Y+d.Z*d.Z),d);}
 public SceneObject PlaceProp(PropAsset asset,Vec3 position,int floor){return new SceneObject{Id=Guid.NewGuid(),Name=asset.Name,Type=SceneObjectType.Prop,Position=position,Rotation=new Vec3(0,0,0),Scale=new Vec3(1,1,1),FloorLevel=floor,Collision=true};}
 public IEnumerable<SceneObject> FloorObjects(ShellProject project,int floor)=>project.Objects.Where(x=>x.FloorLevel==floor);
}
