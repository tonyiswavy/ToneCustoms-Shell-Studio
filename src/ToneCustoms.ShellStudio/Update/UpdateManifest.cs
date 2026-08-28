namespace ToneCustoms.ShellStudio.Update;
public sealed record UpdateManifest(string Version,string? DownloadUrl,string? Sha256,string Channel="stable");
