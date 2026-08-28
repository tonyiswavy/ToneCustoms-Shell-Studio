using ToneCustoms.ShellStudio.Collision;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Preview;
public sealed class CollisionPreviewService { public IReadOnlyList<CollisionBox> Build(ShellProject p)=>new CollisionService().Generate(p).ToList(); }
