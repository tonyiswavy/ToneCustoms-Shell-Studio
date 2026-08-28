using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Materials;
public sealed record TextureBudgetReport(long ReferencedBytes,int ReferencedFiles);
public sealed class TextureBudget { public TextureBudgetReport Analyze(ShellProject p){var files=p.Materials.SelectMany(x=>new[]{x.DiffuseDds,x.NormalDds,x.SpecularDds}).Where(x=>!string.IsNullOrWhiteSpace(x)&&File.Exists(x)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();return new(files.Sum(x=>new FileInfo(x!).Length),files.Count);} }
