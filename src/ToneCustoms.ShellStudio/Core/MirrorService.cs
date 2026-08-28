using ToneCustoms.ShellStudio.Editor;
namespace ToneCustoms.ShellStudio.Core;
public enum MirrorAxis{X,Y,Z}
public sealed class MirrorService { public SceneObject Mirror(SceneObject s,MirrorAxis axis){var p=s.Position;var r=s.Rotation;if(axis==MirrorAxis.X)p.X=-p.X;if(axis==MirrorAxis.Y)p.Y=-p.Y;if(axis==MirrorAxis.Z)p.Z=-p.Z;return new(){Name=s.Name+" Mirrored",Type=s.Type,Position=p,Rotation=r,Scale=s.Scale,MaterialId=s.MaterialId,Collision=s.Collision,FloorLevel=s.FloorLevel};} }
