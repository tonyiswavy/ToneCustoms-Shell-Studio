using Microsoft.Win32;
namespace ToneCustoms.ShellStudio.Bridges;
public sealed record ToolStatus(string Name,bool Found,string? Path,string? Version);
public sealed class DependencyDetector {
 public IEnumerable<ToolStatus> Scan(){yield return FindBlender();yield return FindExecutable("CodeWalker","CodeWalker.exe");yield return new("Sollumz",FindSollumz(),null,null);}
 ToolStatus FindBlender(){var candidates=new[]{@"C:\Program Files\Blender Foundation",Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)+@"\Blender Foundation"};var exe=candidates.Where(Directory.Exists).SelectMany(x=>Directory.GetFiles(x,"blender.exe",SearchOption.AllDirectories)).OrderDescending().FirstOrDefault();return new("Blender",exe!=null,exe,null);}
 ToolStatus FindExecutable(string n,string exe){var paths=(Environment.GetEnvironmentVariable("PATH")??"").Split(';').Select(x=>Path.Combine(x,exe));var p=paths.FirstOrDefault(File.Exists);return new(n,p!=null,p,null);}
 bool FindSollumz(){var roaming=Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);return Directory.Exists(Path.Combine(roaming,"Blender Foundation","Blender"));}
}
