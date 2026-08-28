using System.Windows;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio;
public partial class App:Application { protected override void OnStartup(StartupEventArgs e){ProjectPaths.Ensure();base.OnStartup(e);} }
