using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Props;
public sealed class PropPlacementService { public SceneObject Place(PropAsset prop,Vec3 position,int floor=0)=>new(){Name=prop.Name,Type=SceneObjectType.Prop,ModelName=prop.Model,SourcePath=prop.SourcePath,Position=position,Scale=Vec3.One,FloorLevel=floor,Collision=true}; }
