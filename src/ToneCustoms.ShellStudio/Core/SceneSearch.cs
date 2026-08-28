namespace ToneCustoms.ShellStudio.Core;
public sealed class SceneSearch { public IEnumerable<SceneObject> Find(ShellProject p,string q)=>p.Objects.Where(x=>x.Name.Contains(q,StringComparison.OrdinalIgnoreCase)||(x.ModelName?.Contains(q,StringComparison.OrdinalIgnoreCase)??false)); }
