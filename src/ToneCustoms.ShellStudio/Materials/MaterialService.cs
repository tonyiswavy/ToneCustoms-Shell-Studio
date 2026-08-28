using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Textures;
namespace ToneCustoms.ShellStudio.Materials;
public sealed class MaterialService { readonly DdsService dds=new(); public IEnumerable<string> Validate(MaterialAsset m){foreach(var p in new[]{m.DiffuseDds,m.NormalDds,m.SpecularDds}.Where(x=>!string.IsNullOrWhiteSpace(x))){if(!File.Exists(p)){yield return $"Missing texture: {p}";continue;}foreach(var e in dds.Validate(dds.Inspect(p!)))yield return $"{m.Name}: {e}";}} }
