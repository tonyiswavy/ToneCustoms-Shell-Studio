namespace ToneCustoms.ShellStudio.Core;
public sealed class SceneLayer { public string Name {get;set;}="Layer";public bool Visible {get;set;}=true;public bool Locked {get;set;}public HashSet<Guid> Objects {get;set;}=[]; }
public sealed class SceneLayers { readonly List<SceneLayer> layers=[];public IReadOnlyList<SceneLayer> Layers=>layers;public SceneLayer Add(string name){var l=new SceneLayer{Name=name};layers.Add(l);return l;} }
