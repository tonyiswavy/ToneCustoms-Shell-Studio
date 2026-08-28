namespace ToneCustoms.ShellStudio.Core;
public sealed record SceneLayer(string Name,bool Visible,bool Locked,HashSet<Guid> Objects);
public sealed class SceneLayers { readonly List<SceneLayer> layers=[];public IReadOnlyList<SceneLayer> Layers=>layers;public SceneLayer Add(string name)=>Add(new(name,true,false,new()));SceneLayer Add(SceneLayer l){layers.Add(l);return l;} }
