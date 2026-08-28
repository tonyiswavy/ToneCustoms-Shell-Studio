using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class FloorVisibilityService { public IEnumerable<SceneObject> Visible(ShellProject p,int active,bool isolate)=>isolate?p.Objects.Where(x=>x.FloorLevel==active):p.Objects.Where(x=>x.FloorLevel<=active); }
