namespace ToneCustoms.ShellStudio.Core;
public sealed class ProjectMigration { public const int Current=1;public ShellProject Upgrade(ShellProject p){if(p.Version>Current)throw new NotSupportedException("Project was created by a newer Shell Studio format");p.Version=Current;return p;} }
