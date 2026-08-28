using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Collision;
public sealed record CollisionBox(Guid ObjectId,float X,float Y,float Z,float Width,float Depth,float Height);
public sealed class CollisionService { public IEnumerable<CollisionBox> Generate(ShellProject p)=>p.Objects.Where(o=>o.Collision).Select(o=>new CollisionBox(o.Id,o.Position.X,o.Position.Y,o.Position.Z,Math.Abs(o.Scale.X),Math.Abs(o.Scale.Y),Math.Abs(o.Scale.Z))); }
