using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Export;
public sealed class FiveMResourceBuilder {
 public string Prepare(ShellProject p,string output){var safe=string.Concat(p.Name.ToLowerInvariant().Select(c=>char.IsLetterOrDigit(c)||c=='_'?c:'_'));var root=Path.Combine(output,safe);Directory.CreateDirectory(Path.Combine(root,"stream"));File.WriteAllText(Path.Combine(root,"fxmanifest.lua"),"fx_version 'cerulean'\ngame 'gta5'\nthis_is_a_map 'yes'\n\nfiles { 'stream/*' }\n");File.WriteAllText(Path.Combine(root,"shellstudio.json"),System.Text.Json.JsonSerializer.Serialize(p,new System.Text.Json.JsonSerializerOptions{WriteIndented=true}));return root;}
}
