using System.Text.Json;
namespace ToneCustoms.ShellStudio.Core;
public sealed class SceneCloneService { public ShellProject Clone(ShellProject p)=>JsonSerializer.Deserialize<ShellProject>(JsonSerializer.Serialize(p))??new ShellProject(); }
