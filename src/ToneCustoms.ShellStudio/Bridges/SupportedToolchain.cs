namespace ToneCustoms.ShellStudio.Bridges;
public sealed record ToolchainTarget(string Blender,string Sollumz);
public static class SupportedToolchain
{
 public static readonly ToolchainTarget Primary=new("4.5","2.9.0");
 public static bool BlenderCompatible(string? version)=>!string.IsNullOrWhiteSpace(version)&&version.StartsWith("4.5",StringComparison.OrdinalIgnoreCase);
 public static bool SollumzCompatible(string? version)=>Normalize(version)=="2.9.0";
 static string Normalize(string? value)=>string.Join('.',(value??"").Split('.',StringSplitOptions.RemoveEmptyEntries).Take(3));
 public static string Describe()=> $"Blender {Primary.Blender} / Sollumz {Primary.Sollumz}";
}
