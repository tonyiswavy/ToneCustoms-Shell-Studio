using ToneCustoms.ShellStudio.Bridges;
namespace ToneCustoms.ShellStudio.Validation;
public sealed record CompatibilityResult(bool Compatible,string Message);
public sealed class ToolchainCompatibility { public CompatibilityResult Check(IReadOnlyList<ToolStatus> tools){var blender=tools.FirstOrDefault(x=>x.Name=="Blender");var sollumz=tools.FirstOrDefault(x=>x.Name=="Sollumz");if(blender?.Found!=true)return new(false,"Blender is not configured");if(sollumz?.Found!=true)return new(false,"Sollumz is not configured");return new(true,$"Detected Blender and Sollumz{(string.IsNullOrWhiteSpace(sollumz.Version)?"":" "+sollumz.Version)}. A version adapter must pass export verification before final output.");} }
