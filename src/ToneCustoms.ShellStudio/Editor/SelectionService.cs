using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class SelectionService { readonly HashSet<Guid> ids=[];public IReadOnlyCollection<Guid> Selected=>ids;public void Select(Guid id,bool additive=false){if(!additive)ids.Clear();ids.Add(id);}public void Clear()=>ids.Clear();public IEnumerable<SceneObject> Resolve(ShellProject p)=>p.Objects.Where(x=>ids.Contains(x.Id)); }
