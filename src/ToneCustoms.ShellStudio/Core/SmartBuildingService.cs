namespace ToneCustoms.ShellStudio.Core;
public sealed class SmartBuildingService
{
    readonly GeometryService geometry=new();
    public float LevelElevation(int level,float height=3f)=>level*height;
    public string LevelName(int level)=>level<0?$"Basement {Math.Abs(level)}":level==0?"Ground Floor":$"Floor {level+1}";
    public IReadOnlyList<SceneObject> CreateConnectedWalls(IEnumerable<Vec3> points,float height=3f,float thickness=.2f,int floor=0)
    {
        var pts=points.ToList();var result=new List<SceneObject>();if(pts.Count<2)return result;
        for(var i=0;i<pts.Count-1;i++){var a=pts[i];var b=pts[i+1];var dx=b.X-a.X;var dy=b.Y-a.Y;var length=MathF.Sqrt(dx*dx+dy*dy);if(length<.05f)continue;var center=new Vec3((a.X+b.X)/2,(a.Y+b.Y)/2,LevelElevation(floor,height)+height/2);var wall=geometry.Wall(center);wall.Name=$"{LevelName(floor)} Wall {result.Count+1}";wall.Scale=new Vec3(length,thickness,height);wall.Rotation=new Vec3(0,0,MathF.Atan2(dy,dx)*180/MathF.PI);wall.FloorLevel=floor;result.Add(wall);}return result;
    }
    public IReadOnlyList<SceneObject> CreateClosedRoom(Vec3 origin,float width,float depth,float height=3f,float thickness=.2f,int floor=0)
    {
        var x=origin.X;var y=origin.Y;var z=LevelElevation(floor,height);var p=new[]{new Vec3(x-width/2,y-depth/2,z),new Vec3(x+width/2,y-depth/2,z),new Vec3(x+width/2,y+depth/2,z),new Vec3(x-width/2,y+depth/2,z),new Vec3(x-width/2,y-depth/2,z)};var objects=CreateConnectedWalls(p,height,thickness,floor).ToList();var floorObj=geometry.Floor(new Vec3(x,y,z));floorObj.Name=$"{LevelName(floor)} Slab";floorObj.Scale=new Vec3(width,depth,.12f);floorObj.FloorLevel=floor;objects.Add(floorObj);var ceiling=geometry.Ceiling(new Vec3(x,y,z+height));ceiling.Name=$"{LevelName(floor)} Ceiling";ceiling.Scale=new Vec3(width,depth,.12f);ceiling.FloorLevel=floor;objects.Add(ceiling);return objects;
    }
    public IReadOnlyList<SceneObject> AddAdjacentLevel(ShellProject project,int sourceLevel,int direction,float levelHeight=3f)
    {
        direction=direction<0?-1:1;var next=sourceLevel+direction;var dz=levelHeight*direction;var created=new List<SceneObject>();foreach(var source in project.Objects.Where(x=>x.FloorLevel==sourceLevel&&x.Type is SceneObjectType.Wall or SceneObjectType.Floor or SceneObjectType.Ceiling)){var o=new SceneObject{Name=$"{LevelName(next)} {source.Type}",Type=source.Type,Position=new Vec3(source.Position.X,source.Position.Y,source.Position.Z+dz),Rotation=new Vec3(source.Rotation.X,source.Rotation.Y,source.Rotation.Z),Scale=new Vec3(source.Scale.X,source.Scale.Y,source.Scale.Z),MaterialId=source.MaterialId,Collision=source.Collision,FloorLevel=next};created.Add(o);}return created;
    }
    public IReadOnlyList<SceneObject> AddFloorFromBelow(ShellProject project,int sourceFloor,float floorHeight=3f)=>AddAdjacentLevel(project,sourceFloor,1,floorHeight);
    public IReadOnlyList<SceneObject> AddBasementBelow(ShellProject project,int sourceFloor,float floorHeight=3f)=>AddAdjacentLevel(project,sourceFloor,-1,floorHeight);
    public Vec3 SnapEndpoint(Vec3 point,IEnumerable<SceneObject> walls,float threshold=.35f)
    {
        var best=point;var bestDistance=threshold;foreach(var wall in walls.Where(x=>x.Type==SceneObjectType.Wall)){var half=wall.Scale.X/2;var r=wall.Rotation.Z*MathF.PI/180;var c=MathF.Cos(r);var s=MathF.Sin(r);foreach(var sign in new[]{-1f,1f}){var p=new Vec3(wall.Position.X+c*half*sign,wall.Position.Y+s*half*sign,wall.Position.Z);var dx=point.X-p.X;var dy=point.Y-p.Y;var d=MathF.Sqrt(dx*dx+dy*dy);if(d<bestDistance){best=p;bestDistance=d;}}}return best;
    }
}
