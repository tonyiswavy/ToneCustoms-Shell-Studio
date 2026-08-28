using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed record ScreenPoint(double X,double Y,double Depth);
public sealed class PerspectiveProjection { public ScreenPoint Project(Vec3 p,double width,double height,double yaw=.65,double pitch=.55,double zoom=55){var cy=Math.Cos(yaw);var sy=Math.Sin(yaw);var x=p.X*cy-p.Y*sy;var y=p.X*sy+p.Y*cy;var cp=Math.Cos(pitch);var sp=Math.Sin(pitch);var yy=y*cp-p.Z*sp;var z=y*sp+p.Z*cp;return new(width/2+x*zoom,height/2-yy*zoom,z);} }
