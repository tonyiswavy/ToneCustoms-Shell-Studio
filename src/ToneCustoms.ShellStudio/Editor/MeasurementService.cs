using System.Numerics;
namespace ToneCustoms.ShellStudio.Editor;
public sealed record Measurement(Vector3 A,Vector3 B,float Meters);
public sealed class MeasurementService { public Measurement Measure(Vector3 a,Vector3 b)=>new(a,b,Vector3.Distance(a,b)); }
