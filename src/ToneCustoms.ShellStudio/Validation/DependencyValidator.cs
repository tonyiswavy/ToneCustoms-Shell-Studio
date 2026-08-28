using ToneCustoms.ShellStudio.Bridges;
namespace ToneCustoms.ShellStudio.Validation;
public sealed class DependencyValidator { public IReadOnlyList<string> Validate(IEnumerable<ToolStatus> tools){var r=new List<string>();foreach(var t in tools)if(!t.Found)r.Add($"{t.Name} not detected");return r;} }
