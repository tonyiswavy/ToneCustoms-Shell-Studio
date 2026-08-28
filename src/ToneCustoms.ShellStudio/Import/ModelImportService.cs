namespace ToneCustoms.ShellStudio.Import;
public sealed record ImportedModel(string Path,string Extension,long Bytes);
public sealed class ModelImportService { static readonly HashSet<string> Allowed=new(StringComparer.OrdinalIgnoreCase){".glb",".gltf",".fbx",".obj",".ydr",".ydd"}; public ImportedModel Inspect(string path){if(!File.Exists(path))throw new FileNotFoundException(path);var ext=Path.GetExtension(path);if(!Allowed.Contains(ext))throw new NotSupportedException($"Unsupported model format {ext}");return new(path,ext,new FileInfo(path).Length);} }
