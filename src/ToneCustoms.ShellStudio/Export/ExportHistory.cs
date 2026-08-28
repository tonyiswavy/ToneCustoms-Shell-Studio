using System.Text.Json;
namespace ToneCustoms.ShellStudio.Export;
public sealed class ExportHistory { public void Append(ExportReport report,string root){Directory.CreateDirectory(root);var path=Path.Combine(root,"export-history.jsonl");File.AppendAllText(path,JsonSerializer.Serialize(report)+Environment.NewLine);} }
