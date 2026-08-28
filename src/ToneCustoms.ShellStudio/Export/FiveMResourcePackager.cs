using System.Text;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Export;
public sealed record ResourcePackageResult(bool Success,string ResourcePath,IReadOnlyList<string> Issues);
public sealed class FiveMResourcePackager
{
 public ResourcePackageResult Build(ShellProject project,string output,IEnumerable<string> gtaFiles){var issues=new List<string>();var safe=SafeName(project.Name);if(string.IsNullOrWhiteSpace(safe))safe="tonecustoms_shell";var root=Path.Combine(output,safe);var stream=Path.Combine(root,"stream");Directory.CreateDirectory(stream);var files=gtaFiles.Where(File.Exists).Distinct(StringComparer.OrdinalIgnoreCase).ToList();if(files.Count==0)issues.Add("No verified GTA stream files were supplied.");foreach(var f in files)File.Copy(f,Path.Combine(stream,Path.GetFileName(f)),true);var manifest=new StringBuilder().AppendLine("fx_version 'cerulean'").AppendLine("game 'gta5'").AppendLine().AppendLine("this_is_a_map 'yes'").ToString();File.WriteAllText(Path.Combine(root,"fxmanifest.lua"),manifest);File.WriteAllText(Path.Combine(root,"shellstudio-export.txt"),$"Project: {project.Name}\nObjects: {project.Objects.Count}\nGenerated: {DateTimeOffset.UtcNow:O}\n");return new(issues.Count==0,root,issues);}
 static string SafeName(string s)=>new(s.Trim().ToLowerInvariant().Select(c=>char.IsLetterOrDigit(c)||c=='_'?c:'_').ToArray()).Trim('_');
}
