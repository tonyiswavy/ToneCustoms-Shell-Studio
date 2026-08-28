namespace ToneCustoms.ShellStudio.Import;
public sealed record PropImportCheck(bool Valid,string Format,string Message,long SizeBytes);
public sealed class PropImportValidator
{
 static readonly HashSet<string> Formats=new(StringComparer.OrdinalIgnoreCase){".glb",".gltf",".fbx",".obj",".ydr"};
 public PropImportCheck Validate(string path){if(!File.Exists(path))return new(false,"","File does not exist.",0);var info=new FileInfo(path);var ext=info.Extension.ToLowerInvariant();if(!Formats.Contains(ext))return new(false,ext,"Unsupported model format. Use GLB, GLTF, FBX, OBJ, or YDR.",info.Length);if(info.Length==0)return new(false,ext,"Model file is empty.",0);if(info.Length>250L*1024*1024)return new(false,ext,"Model exceeds the 250 MB import safety limit.",info.Length);return new(true,ext,"Ready for import.",info.Length);}
}
