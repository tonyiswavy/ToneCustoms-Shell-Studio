using System.IO;
namespace ToneCustoms.ShellStudio.Core;
public sealed class MaterialService
{
    static readonly string[] Allowed={".dds",".png",".tga"};
    public MaterialRecord Create(string name,string texturePath,string? normalPath=null,string? roughnessPath=null)
    {
        if(string.IsNullOrWhiteSpace(name))throw new ArgumentException("Material name is required.");
        ValidateTexture(texturePath);if(!string.IsNullOrWhiteSpace(normalPath))ValidateTexture(normalPath);if(!string.IsNullOrWhiteSpace(roughnessPath))ValidateTexture(roughnessPath);
        return new MaterialRecord(Guid.NewGuid(),name,texturePath,normalPath,roughnessPath);
    }
    public IReadOnlyList<string> Validate(MaterialRecord material)
    {
        var issues=new List<string>();Check(material.BaseColor, "Base color",issues);CheckOptional(material.Normal,"Normal",issues);CheckOptional(material.Roughness,"Roughness",issues);return issues;
    }
    static void ValidateTexture(string path){if(!Allowed.Contains(Path.GetExtension(path),StringComparer.OrdinalIgnoreCase))throw new InvalidDataException("Texture must be DDS, PNG, or TGA.");}
    static void Check(string path,string label,List<string> issues){if(string.IsNullOrWhiteSpace(path)){issues.Add($"{label} texture missing.");return;}ValidateTexture(path);if(!File.Exists(path))issues.Add($"{label} file not found: {path}");}
    static void CheckOptional(string? path,string label,List<string> issues){if(string.IsNullOrWhiteSpace(path))return;ValidateTexture(path);if(!File.Exists(path))issues.Add($"{label} file not found: {path}");}
}
public sealed record MaterialRecord(Guid Id,string Name,string BaseColor,string? Normal,string? Roughness);
