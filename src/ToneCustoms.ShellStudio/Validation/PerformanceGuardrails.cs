using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public sealed class PerformanceGuardrails { public IEnumerable<string> Check(ShellProject p){if(p.Objects.Count>1500)yield return "Scene exceeds 1500 editor objects";if(p.Materials.Count>128)yield return "Scene exceeds 128 materials";if(p.Objects.Count(x=>x.Collision)>1000)yield return "Collision object count is very high";} }
