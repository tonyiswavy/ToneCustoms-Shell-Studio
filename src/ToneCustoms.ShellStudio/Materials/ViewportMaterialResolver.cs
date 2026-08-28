using System.Windows.Media;using System.Windows.Media.Imaging;using System.Windows.Media.Media3D;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Materials;
public sealed class ViewportMaterialResolver
{
 public Material Resolve(ShellProject project,SceneObject obj,Color fallback){var asset=project.Materials.FirstOrDefault(x=>x.Id==obj.MaterialId);if(asset==null||string.IsNullOrWhiteSpace(asset.DiffuseDds)||!File.Exists(asset.DiffuseDds))return Solid(fallback);var ext=Path.GetExtension(asset.DiffuseDds).ToLowerInvariant();if(ext==".png"){try{var image=new BitmapImage();image.BeginInit();image.CacheOption=BitmapCacheOption.OnLoad;image.UriSource=new Uri(asset.DiffuseDds,UriKind.Absolute);image.EndInit();image.Freeze();var brush=new ImageBrush(image){TileMode=TileMode.Tile,ViewportUnits=BrushMappingMode.RelativeToBoundingBox,Viewport=new System.Windows.Rect(0,0,1/Math.Max(.01,asset.Tiling),1/Math.Max(.01,asset.Tiling))};brush.Freeze();return new DiffuseMaterial(brush);}catch{return Solid(fallback);}}return Solid(fallback);}
 static Material Solid(Color c){var b=new SolidColorBrush(c);b.Freeze();return new DiffuseMaterial(b);}
 public string PreviewStatus(ShellProject project,SceneObject obj){var a=project.Materials.FirstOrDefault(x=>x.Id==obj.MaterialId);if(a==null)return "No material";if(string.IsNullOrWhiteSpace(a.DiffuseDds))return $"{a.Name}: no diffuse texture";return Path.GetExtension(a.DiffuseDds).Equals(".dds",StringComparison.OrdinalIgnoreCase)?$"{a.Name}: DDS assigned (GTA export; native viewport decoder pending)":$"{a.Name}: preview ready";}
}
