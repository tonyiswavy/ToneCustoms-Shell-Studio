using ToneCustoms.ShellStudio.Core;using ToneCustoms.ShellStudio.Textures;
namespace ToneCustoms.ShellStudio.Materials;
public sealed class DdsMaterialLibrary { readonly DdsService dds=new();public MaterialAsset Create(string name,string diffuse,string? normal=null,string? specular=null){foreach(var p in new[]{diffuse,normal,specular}.Where(x=>!string.IsNullOrWhiteSpace(x))){var i=dds.Inspect(p!);var errors=dds.Validate(i).ToArray();if(errors.Length>0)throw new InvalidDataException(string.Join("; ",errors));}return new(){Name=name,DiffuseDds=diffuse,NormalDds=normal,SpecularDds=specular};} }
