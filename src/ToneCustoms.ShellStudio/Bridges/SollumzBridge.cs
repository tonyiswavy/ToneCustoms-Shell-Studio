namespace ToneCustoms.ShellStudio.Bridges;
public sealed class SollumzBridge { public string BuildExportArguments(string projectJson,string output)=>$"export --project \"{projectJson}\" --output \"{output}\" --collision --bounds --materials"; }
