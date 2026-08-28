using System.Diagnostics;using System.Text.Json;using ToneCustoms.ShellStudio.Bridges;using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Validation;
namespace ToneCustoms.ShellStudio.Export;
public sealed record NativeGtaExportResult(bool Success,string? ResourcePath,IReadOnlyList<string> GtaFiles,IReadOnlyList<string> Issues);
public sealed class NativeGtaExportService
{
 public async Task<NativeGtaExportResult> ExportAsync(ShellProject project,string output,string blenderExe,string bridgeScript,CancellationToken cancellationToken=default)
 {
  var issues=new List<string>();if(!File.Exists(blenderExe))issues.Add("Blender executable was not found.");if(!File.Exists(bridgeScript))issues.Add("Shell Studio Blender export bridge was not found.");if(issues.Count>0)return new(false,null,[],issues);
  var work=Path.Combine(ProjectPaths.AppRoot,"native-export",DateTime.UtcNow.ToString("yyyyMMdd-HHmmssfff"));var gtaOut=Path.Combine(work,"gta");Directory.CreateDirectory(gtaOut);var projectFile=Path.Combine(work,"project.tcshell");await File.WriteAllTextAsync(projectFile,JsonSerializer.Serialize(project,new JsonSerializerOptions{WriteIndented=true}),cancellationToken);
  var psi=new ProcessStartInfo(blenderExe){UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true,RedirectStandardError=true};psi.ArgumentList.Add("--background");psi.ArgumentList.Add("--python");psi.ArgumentList.Add(bridgeScript);psi.ArgumentList.Add("--");psi.ArgumentList.Add(projectFile);psi.ArgumentList.Add(gtaOut);
  using var p=Process.Start(psi);if(p==null)return new(false,null,[],["Blender could not be started."]);var stdout=p.StandardOutput.ReadToEndAsync(cancellationToken);var stderr=p.StandardError.ReadToEndAsync(cancellationToken);await p.WaitForExitAsync(cancellationToken);var err=await stderr;var status=Path.Combine(gtaOut,"bridge-status.json");if(!File.Exists(status))return new(false,null,[],["Blender did not produce bridge-status.json.",err]);
  using var doc=JsonDocument.Parse(await File.ReadAllTextAsync(status,cancellationToken));var root=doc.RootElement;var completed=root.TryGetProperty("gta_export_completed",out var done)&&done.GetBoolean();var files=root.TryGetProperty("gta_files",out var arr)&&arr.ValueKind==JsonValueKind.Array?arr.EnumerateArray().Select(x=>x.GetString()).Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x=>Path.Combine(gtaOut,x!)).Where(File.Exists).ToList():[];
  if(!completed||files.Count==0){issues.Add("Sollumz did not produce verified native GTA files.");if(!string.IsNullOrWhiteSpace(err))issues.Add(err.Trim());return new(false,null,files,issues);}
  issues.AddRange(new GtaAssetValidator().ValidateFiles(files));if(issues.Count>0)return new(false,null,files,issues);var package=new FiveMResourcePackager().Build(project,output,files);issues.AddRange(package.Issues);return new(package.Success,package.ResourcePath,files,issues);
 }
}
