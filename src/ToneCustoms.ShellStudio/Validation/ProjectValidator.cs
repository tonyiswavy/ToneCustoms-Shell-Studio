using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public enum ValidationSeverity { Info, Warning, Error }
public sealed record ValidationIssue(ValidationSeverity Severity,string Area,string Message,Guid? ObjectId=null);
public sealed class ProjectValidator {
 public IReadOnlyList<ValidationIssue> Validate(ShellProject p){var r=new List<ValidationIssue>();if(p.Objects.Count==0)r.Add(new(ValidationSeverity.Warning,"Geometry","Project contains no geometry"));foreach(var o in p.Objects){if(o.Scale.X<=0||o.Scale.Y<=0||o.Scale.Z<=0)r.Add(new(ValidationSeverity.Error,"Geometry",$"{o.Name} has invalid scale",o.Id));if(string.IsNullOrWhiteSpace(o.MaterialId)&&o.Type is not SceneObjectType.Light)r.Add(new(ValidationSeverity.Warning,"Materials",$"{o.Name} has no material",o.Id));}return r;}
 public void AutoFixSafe(ShellProject p){foreach(var o in p.Objects){o.Scale=new(Math.Max(.01f,o.Scale.X),Math.Max(.01f,o.Scale.Y),Math.Max(.01f,o.Scale.Z));}}
}
