using System.Numerics;
namespace ToneCustoms.ShellStudio.Core;
public sealed class GeometryService {
 public SceneObject Wall(Vector3 p,float length=4,float height=3,float thickness=.15f)=>New("Wall",SceneObjectType.Wall,p,new(length,thickness,height));
 public SceneObject Floor(Vector3 p,float width=4,float depth=4,float thickness=.1f)=>New("Floor",SceneObjectType.Floor,p,new(width,depth,thickness));
 public SceneObject Ceiling(Vector3 p,float width=4,float depth=4,float thickness=.1f)=>New("Ceiling",SceneObjectType.Ceiling,p,new(width,depth,thickness));
 public SceneObject Door(Vector3 p)=>New("Door",SceneObjectType.Door,p,new(.9f,.15f,2.1f));
 public SceneObject Window(Vector3 p)=>New("Window",SceneObjectType.Window,p,new(1.2f,.15f,1.2f));
 public SceneObject Stairs(Vector3 p)=>New("Stairs",SceneObjectType.Stairs,p,new(2,3,1.5f));
 public SceneObject Room(Vector3 p,float width=5,float depth=5,float height=3)=>New("Room",SceneObjectType.Room,p,new(width,depth,height));
 static SceneObject New(string n,SceneObjectType t,Vector3 p,Vector3 s)=>new(){Name=n,Type=t,Position=p,Scale=s};
 public Vector3 Snap(Vector3 v,float grid)=>new(MathF.Round(v.X/grid)*grid,MathF.Round(v.Y/grid)*grid,MathF.Round(v.Z/grid)*grid);
}
