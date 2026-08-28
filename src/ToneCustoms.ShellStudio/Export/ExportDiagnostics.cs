namespace ToneCustoms.ShellStudio.Export;
public sealed record ExportStep(string Name,bool Success,string Message,string? LogPath=null);
public sealed class ExportDiagnostics { readonly List<ExportStep> steps=[];public IReadOnlyList<ExportStep> Steps=>steps;public bool Success=>steps.Count>0&&steps.All(x=>x.Success);public void Add(string name,bool success,string message,string? log=null)=>steps.Add(new(name,success,message,log)); }
