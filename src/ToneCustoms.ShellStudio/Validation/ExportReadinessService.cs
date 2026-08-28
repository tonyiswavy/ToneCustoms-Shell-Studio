using ToneCustoms.ShellStudio.Bridges;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public sealed class ExportReadinessService { readonly ProjectValidator projectValidator=new();public IReadOnlyList<string> Check(ShellProject p,IEnumerable<ToolStatus> tools){var r=projectValidator.Validate(p).Where(x=>x.Severity==ValidationSeverity.Error).Select(x=>x.Message).ToList();foreach(var n in new[]{"Blender","Sollumz"})if(!tools.Any(x=>x.Name==n&&x.Found))r.Add(n+" is required for final GTA export");return r;} }
