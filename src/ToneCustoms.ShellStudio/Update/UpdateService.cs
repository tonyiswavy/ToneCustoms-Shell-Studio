namespace ToneCustoms.ShellStudio.Update;
public sealed class UpdateService { public Version Current=>typeof(UpdateService).Assembly.GetName().Version??new Version(0,0); public bool IsNewer(string version)=>Version.TryParse(version,out var v)&&v>Current; }
