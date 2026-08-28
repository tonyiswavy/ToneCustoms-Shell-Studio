namespace ToneCustoms.ShellStudio.Update;
public enum ReleaseChannel { Stable, Preview }
public sealed class ReleaseChannelSettings { public ReleaseChannel Channel {get;set;}=ReleaseChannel.Stable;public bool AutomaticChecks {get;set;}=true; }
