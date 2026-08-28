namespace ToneCustoms.ShellStudio.Core;
public static class CrashRecovery { public static void Register(Func<ShellProject> project){AppDomain.CurrentDomain.UnhandledException+=(s,e)=>{try{ProjectPaths.Ensure();new ProjectService().AutosaveAsync(project(),ProjectPaths.AppRoot).GetAwaiter().GetResult();}catch{}};} }
