namespace ToneCustoms.ShellStudio.Export;
public sealed class ResourceValidationService { public IReadOnlyList<string> ValidateFolder(string root){var r=new List<string>();if(!File.Exists(Path.Combine(root,"fxmanifest.lua")))r.Add("fxmanifest.lua missing");if(!Directory.Exists(Path.Combine(root,"stream")))r.Add("stream folder missing");return r;} }
