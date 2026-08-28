using System.Diagnostics;using System.Text.Json;
namespace ToneCustoms.ShellStudio.Bridges;
public sealed record ToolchainProbeResult(bool Success,string Message,string? BlenderVersion,string? SollumzVersion,string[] Operators,string? StatusFile);
public sealed class AutomaticToolchainProbe
{
 public async Task<ToolchainProbeResult> RunAsync(string blenderExe,string bridgeScript,string workingDirectory,CancellationToken cancellationToken=default)
 {
  if(!File.Exists(blenderExe))return new(false,"Blender executable was not found.",null,null,[],null);
  if(!File.Exists(bridgeScript))return new(false,"Shell Studio Blender bridge was not found.",null,null,[],null);
  Directory.CreateDirectory(workingDirectory);var project=Path.Combine(workingDirectory,"probe-project.json");var output=Path.Combine(workingDirectory,"probe-output");Directory.CreateDirectory(output);
  await File.WriteAllTextAsync(project,"{\"Name\":\"Toolchain Probe\",\"Objects\":[]}",cancellationToken);
  var psi=new ProcessStartInfo(blenderExe){UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true,RedirectStandardError=true};psi.ArgumentList.Add("--background");psi.ArgumentList.Add("--python");psi.ArgumentList.Add(bridgeScript);psi.ArgumentList.Add("--");psi.ArgumentList.Add(project);psi.ArgumentList.Add(output);
  using var p=Process.Start(psi);if(p==null)return new(false,"Blender could not be started.",null,null,[],null);var stdout=p.StandardOutput.ReadToEndAsync(cancellationToken);var stderr=p.StandardError.ReadToEndAsync(cancellationToken);await p.WaitForExitAsync(cancellationToken);var status=Path.Combine(output,"bridge-status.json");if(!File.Exists(status))return new(false,"Blender ran but did not produce toolchain status: "+await stderr,null,null,[],null);
  using var doc=JsonDocument.Parse(await File.ReadAllTextAsync(status,cancellationToken));var root=doc.RootElement;string? blender=root.TryGetProperty("blender",out var b)?b.GetString():null;string? sollumz=root.TryGetProperty("sollumz_version",out var s)?s.GetString():null;var ops=root.TryGetProperty("sollumz_operators",out var o)&&o.ValueKind==JsonValueKind.Array?o.EnumerateArray().Select(x=>x.GetString()).Where(x=>!string.IsNullOrWhiteSpace(x)).Cast<string>().ToArray():[];var loaded=root.TryGetProperty("sollumz_loaded",out var l)&&l.GetBoolean();var compatible=root.TryGetProperty("toolchain_compatible",out var c)&&c.GetBoolean();var ok=loaded&&compatible&&ops.Length>0;return new(ok,ok?"Blender 4.5 + Sollumz 2.9.0 detected and ready for adapter selection.":"Toolchain detected but is not ready. Check Blender/Sollumz installation.",blender,sollumz,ops,status);
 }
}
