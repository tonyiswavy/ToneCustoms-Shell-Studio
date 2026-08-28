using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class MultiFloorService { public const float DefaultFloorHeight=3f;public IEnumerable<SceneObject> OnFloor(ShellProject p,int floor)=>p.Objects.Where(x=>x.FloorLevel==floor);public void MoveToFloor(SceneObject o,int floor){o.FloorLevel=floor;o.Position=new(o.Position.X,o.Position.Y,floor*DefaultFloorHeight);} }
