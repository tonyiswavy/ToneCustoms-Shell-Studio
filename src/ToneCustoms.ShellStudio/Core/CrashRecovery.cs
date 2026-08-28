namespace ToneCustoms.ShellStudio.Core;
public static class CrashRecovery { static bool registered;public static void Register(Func<ShellProject> project){if(registered)return;registered=true;AppDomain.CurrentDomain.UnhandledException+=(s,e)=>{try{ProjectPaths.Ensure();new ProjectService().AutosaveAsync(project(),ProjectPaths.AppRoot).GetAwaiter().GetResult();}catch{}};} }
