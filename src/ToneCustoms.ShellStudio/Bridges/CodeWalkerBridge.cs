using System.Diagnostics;
namespace ToneCustoms.ShellStudio.Bridges;
public sealed class CodeWalkerBridge { public void Open(string exe,string target){Process.Start(new ProcessStartInfo(exe,$"\"{target}\""){UseShellExecute=true});} }
