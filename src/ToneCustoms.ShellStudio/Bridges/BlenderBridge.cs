using System.Diagnostics;
namespace ToneCustoms.ShellStudio.Bridges;
public sealed class BlenderBridge {
 public async Task<int> RunHeadlessAsync(string blender,string blendOrEmpty,string script,string args){var psi=new ProcessStartInfo(blender){UseShellExecute=false,RedirectStandardOutput=true,RedirectStandardError=true,CreateNoWindow=true};psi.ArgumentList.Add("--background");if(!string.IsNullOrWhiteSpace(blendOrEmpty))psi.ArgumentList.Add(blendOrEmpty);psi.ArgumentList.Add("--python");psi.ArgumentList.Add(script);psi.ArgumentList.Add("--");psi.ArgumentList.Add(args);using var p=Process.Start(psi)??throw new InvalidOperationException("Unable to start Blender");await p.WaitForExitAsync();return p.ExitCode;}
}
