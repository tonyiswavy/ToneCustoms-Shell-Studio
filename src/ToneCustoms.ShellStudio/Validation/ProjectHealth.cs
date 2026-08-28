using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public sealed record ProjectHealth(int Errors,int Warnings,int Infos,string Grade);
public sealed class ProjectHealthService { public ProjectHealth Get(ShellProject p){var x=new ProjectValidator().Validate(p);var e=x.Count(i=>i.Severity==ValidationSeverity.Error);var w=x.Count(i=>i.Severity==ValidationSeverity.Warning);return new(e,w,x.Count-e-w,e>0?"Blocked":w>10?"Needs attention":"Good");} }
