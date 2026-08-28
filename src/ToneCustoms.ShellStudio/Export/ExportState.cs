namespace ToneCustoms.ShellStudio.Export;
public enum ExportState { NotStarted, ProjectValidated, ToolchainValidated, SceneGenerated, GtaConversionCompleted, GtaOutputVerified, ResourceBuilt, Failed }
public sealed record ExportStatus(ExportState State,string Message,string? OutputPath=null);
