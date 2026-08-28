namespace ToneCustoms.ShellStudio.Export;
public sealed record ExportReport(DateTime StartedUtc,DateTime FinishedUtc,bool Success,string Project,string? ResourcePath,IReadOnlyList<ExportStep> Steps,IReadOnlyList<string> OutputErrors);
