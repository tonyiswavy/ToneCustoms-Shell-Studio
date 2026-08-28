using ToneCustoms.ShellStudio.Editor;
namespace ToneCustoms.ShellStudio.Core;
public sealed class RoomDetectionService { public IEnumerable<SceneObject> CandidateWalls(ShellProject p,int floor)=>p.Objects.Where(x=>x.Type==SceneObjectType.Wall&&x.FloorLevel==floor); public bool HasClosableRoom(ShellProject p,int floor)=>CandidateWalls(p,floor).Count()>=4; }
