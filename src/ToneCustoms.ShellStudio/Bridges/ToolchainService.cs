namespace ToneCustoms.ShellStudio.Bridges;
public sealed class ToolchainService { readonly DependencyDetector detector=new(); public IReadOnlyList<ToolStatus> Status()=>detector.Scan().ToList(); public bool ReadyForGtaExport()=>Status().Where(x=>x.Name is "Blender" or "Sollumz").All(x=>x.Found); }
