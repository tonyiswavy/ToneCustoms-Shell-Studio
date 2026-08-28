namespace ToneCustoms.ShellStudio.Core;
public sealed record ObjectGroup(Guid Id,string Name,List<Guid> Objects);
public sealed class ObjectGrouping { readonly List<ObjectGroup> groups=[];public IReadOnlyList<ObjectGroup> Groups=>groups;public ObjectGroup Create(string name,IEnumerable<SceneObject> items){var g=new ObjectGroup(Guid.NewGuid(),name,items.Select(x=>x.Id).Distinct().ToList());groups.Add(g);return g;} }
