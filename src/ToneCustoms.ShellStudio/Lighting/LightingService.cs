using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Lighting;
public sealed class LightingService { public SceneObject AddPreviewLight(Vec3 p,float range=5)=>new(){Name="Preview Light",Type=SceneObjectType.Light,Position=p,Scale=new(range,range,range),Collision=false}; }
