using System.Diagnostics;using System.Text.Json;using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Validation;
namespace ToneCustoms.ShellStudio.Export;
public sealed record NativeExportResult(bool Success,string? ResourcePath,IReadOnlyList<string> Issues,IReadOnlyList<string> GtaFiles);
public sealed class NativeFiveMExportService
{
 public async Task<NativeExportResult> ExportAsync(ShellProject project,string blenderExe,string bridgeScript,string destination,CancellationToken ct=default)
 {
  var issues=new List<string>();if(!File.Exists(blenderExe))issues.Add("Blender executable not found.");if(!File.Exists(bridgeScript))issues.Add("Shell Studio Blender/Sollumz bridge not found.");if(project.Objects.Count==0)issues.Add("Project has no shell geometry to export.");if(issues.Count>0)return new(false,null,issues,[]);
  var work=Path.Combine(Path.GetTempPath(),"ToneCustomsShellStudio","export-"+Guid.NewGuid().ToString("N"));Directory.CreateDirectory(work);var projectFile=Path.Combine(work,"project.tcshell.json");var gtaOut=Path.Combine(work,"gta");Directory.CreateDirectory(gtaOut);await File.WriteAllTextAsync(projectFile,JsonSerializer.Serialize(project,new JsonSerializerOptions{WriteIndented=true}),ct);
  var psi=new ProcessStartInfo(blenderExe){UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true,RedirectStandardError=true};psi.ArgumentList.Add("--background");psi.ArgumentList.Add("--python");psi.ArgumentList.Add(bridgeScript);psi.ArgumentList.Add("--");psi.ArgumentList.Add(projectFile);psi.ArgumentList.Add(gtaOut);
  using var p=Process.Start(psi);if(p==null)return new(false,null,["Blender failed to start."],[]);var stderr=p.StandardError.ReadToEndAsync(ct);await p.WaitForExitAsync(ct);var err=await stderr;var statusFile=Path.Combine(gtaOut,"bridge-status.json");if(!File.Exists(statusFile))return new(false,null,["Blender/Sollumz did not create bridge-status.json.",err],[]);
  using var doc=JsonDocument.Parse(await File.ReadAllTextAsync(statusFile,ct));var root=doc.RootElement;var completed=root.TryGetProperty("gta_export_completed",out var done)&&done.GetBoolean();var gta=root.TryGetProperty("gta_files",out var files)&&files.ValueKind==JsonValueKind.Array?files.EnumerateArray().Select(x=>x.GetString()).Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x=>Path.Combine(gtaOut,x!)).Where(File.Exists).ToList():[];
  var checker=new GtaAssetValidator();issues.AddRange(checker.Validate(gta).Where(x=>!x.Valid).Select(x=>$"{Path.GetFileName(x.File)}: {x.Detail}"));issues.AddRange(checker.PipelineIssues(gta,project.Objects.Any(x=>x.Collision),project.Materials.Count>0));if(!completed||p.ExitCode!=0||gta.Count==0){if(!string.IsNullOrWhiteSpace(err))issues.Add(err.Trim());if(gta.Count==0)issues.Add("No native GTA files were produced by Sollumz.");return new(false,null,issues.Distinct().ToList(),gta);}
  var package=new FiveMResourcePackager().Build(project,destination,gta);issues.AddRange(package.Issues);return new(package.Success&&issues.Count==0,package.ResourcePath,issues,gta);
 }
}
