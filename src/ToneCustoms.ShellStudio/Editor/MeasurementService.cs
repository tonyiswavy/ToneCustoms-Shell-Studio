using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed record Measurement(Vec3 A,Vec3 B,float Meters);
public sealed class MeasurementService { public Measurement Measure(Vec3 a,Vec3 b){var x=a.X-b.X;var y=a.Y-b.Y;var z=a.Z-b.Z;return new(a,b,MathF.Sqrt(x*x+y*y+z*z));} }
