using System.IO;
namespace ToneCustoms.ShellStudio.Core;
public sealed class TexturePackageService
{
    public TexturePackageResult ValidateDirectory(string directory)
    {
        if(!Directory.Exists(directory))return new(false,new[]{"Texture directory does not exist."},Array.Empty<string>());
        var files=Directory.EnumerateFiles(directory,"*",SearchOption.AllDirectories).Where(IsTexture).ToArray();var issues=new List<string>();foreach(var file in files){var info=new FileInfo(file);if(info.Length==0)issues.Add($"Empty texture: {file}");}if(files.Length==0)issues.Add("No supported texture files found.");return new(issues.Count==0,issues,files);
    }
    static bool IsTexture(string path)=>new[]{".dds",".png",".tga"}.Contains(Path.GetExtension(path),StringComparer.OrdinalIgnoreCase);
}
public sealed record TexturePackageResult(bool Success,IReadOnlyList<string> Issues,IReadOnlyList<string> Files);
