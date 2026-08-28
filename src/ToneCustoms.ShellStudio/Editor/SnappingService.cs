using System.Numerics;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed class SnappingService { public Vector3 Grid(Vector3 v,float size)=>new(MathF.Round(v.X/size)*size,MathF.Round(v.Y/size)*size,MathF.Round(v.Z/size)*size); public Vector3 Smart(Vector3 p,IEnumerable<SceneObject> objects,float threshold=.2f){foreach(var o in objects){if(Vector3.Distance(p,o.Position)<=threshold)return o.Position;}return p;} }
