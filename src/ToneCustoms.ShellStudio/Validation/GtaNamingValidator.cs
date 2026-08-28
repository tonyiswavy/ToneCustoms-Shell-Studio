namespace ToneCustoms.ShellStudio.Validation;
public sealed class GtaNamingValidator { public bool IsSafeResourceName(string n)=>!string.IsNullOrWhiteSpace(n)&&n.All(c=>char.IsLetterOrDigit(c)||c=='_'||c=='-');public string Sanitize(string n)=>string.Concat(n.ToLowerInvariant().Select(c=>char.IsLetterOrDigit(c)||c=='_'?c:'_')).Trim('_'); }
