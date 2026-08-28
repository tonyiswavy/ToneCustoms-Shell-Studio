using System.Numerics;
namespace ToneCustoms.ShellStudio.Core;
public sealed record SceneBounds(Vector3 Min,Vector3 Max){public Vector3 Size=>Max-Min;}
public sealed class BoundsService { public SceneBounds Calculate(ShellProject p){if(p.Objects.Count==0)return new(Vector3.Zero,Vector3.Zero);var min=new Vector3(float.MaxValue),max=new Vector3(float.MinValue);foreach(var o in p.Objects){var half=o.Scale/2;min=Vector3.Min(min,o.Position-half);max=Vector3.Max(max,o.Position+half);}return new(min,max);} }
