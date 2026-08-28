namespace ToneCustoms.ShellStudio.Props;
public sealed class PropFavorites { readonly HashSet<string> models=new(StringComparer.OrdinalIgnoreCase);public IEnumerable<string> Models=>models;public bool Toggle(string model)=>models.Contains(model)?!models.Remove(model):models.Add(model);public bool Contains(string model)=>models.Contains(model); }
