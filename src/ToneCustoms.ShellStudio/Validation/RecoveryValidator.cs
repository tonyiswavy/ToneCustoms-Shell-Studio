using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Validation;
public sealed class RecoveryValidator { public bool CanOpen(string path,out string message){try{var p=new ProjectService().LoadAsync(path).GetAwaiter().GetResult();message=$"Recovery project '{p.Name}' is readable";return true;}catch(Exception e){message=e.Message;return false;}} }
