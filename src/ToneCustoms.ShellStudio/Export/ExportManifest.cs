namespace ToneCustoms.ShellStudio.Export;
public sealed record ExportManifest(string Project,string Resource,string CreatedUtc,string[] ExpectedOutputs,string[] Warnings);
