using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Import;
public sealed class ImportPipeline { readonly ModelImportService inspect=new();public SceneObject ImportAsProp(string path,Vec3 position){var m=inspect.Inspect(path);return new(){Name=Path.GetFileNameWithoutExtension(path),Type=SceneObjectType.Prop,Position=position,Scale=Vec3.One,SourcePath=m.Path,ModelName=Path.GetFileNameWithoutExtension(path),Collision=true};} }
