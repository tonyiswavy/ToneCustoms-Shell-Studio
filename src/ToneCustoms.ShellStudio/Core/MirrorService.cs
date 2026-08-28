namespace ToneCustoms.ShellStudio.Core;
public enum MirrorAxis{X,Y,Z}
public sealed class MirrorService { public SceneObject Mirror(SceneObject s,MirrorAxis a){var p=new Vec3(s.Position.X,s.Position.Y,s.Position.Z);if(a==MirrorAxis.X)p.X=-p.X;if(a==MirrorAxis.Y)p.Y=-p.Y;if(a==MirrorAxis.Z)p.Z=-p.Z;return new(){Name=s.Name+" Mirrored",Type=s.Type,Position=p,Rotation=new(s.Rotation.X,s.Rotation.Y,s.Rotation.Z),Scale=new(s.Scale.X,s.Scale.Y,s.Scale.Z),MaterialId=s.MaterialId,Collision=s.Collision,FloorLevel=s.FloorLevel};} }
