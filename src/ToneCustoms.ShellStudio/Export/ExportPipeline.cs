using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Validation;
namespace ToneCustoms.ShellStudio.Export;
public sealed record ExportResult(bool Success,string? ResourcePath,IReadOnlyList<ValidationIssue> Issues);
public sealed class ExportPipeline { readonly ProjectValidator validator=new();readonly FiveMResourceBuilder builder=new();public ExportResult Prepare(ShellProject p,string output){var issues=validator.Validate(p);if(issues.Any(x=>x.Severity==ValidationSeverity.Error))return new(false,null,issues);return new(true,builder.Prepare(p,output),issues);} }
