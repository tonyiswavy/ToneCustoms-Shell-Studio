namespace ToneCustoms.ShellStudio.Bridges;
public sealed class BridgeLog { public string Write(string name,string text){var root=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"ToneCustoms","ShellStudio","Logs");Directory.CreateDirectory(root);var p=Path.Combine(root,$"{DateTime.UtcNow:yyyyMMdd-HHmmss}-{name}.log");File.WriteAllText(p,text);return p;} }
