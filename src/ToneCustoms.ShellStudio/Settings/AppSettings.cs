namespace ToneCustoms.ShellStudio.Settings;
public sealed class AppSettings { public string? BlenderPath {get;set;} public string? CodeWalkerPath {get;set;} public string? FiveMServerResourcesPath {get;set;} public bool SimpleMode {get;set;}=true; public bool AutoSave {get;set;}=true; public int AutoSaveSeconds {get;set;}=120; public bool CheckUpdates {get;set;}=true; }
