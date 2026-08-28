using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Editor;
public sealed record ProjectedObject(Guid Id,string Name,double Left,double Top,double Width,double Height,SceneObjectType Type);
public sealed class SceneProjection { public IEnumerable<ProjectedObject> TopDown(ShellProject p,double pixelsPerMeter=35,double originX=400,double originY=300){foreach(var o in p.Objects){var w=Math.Max(6,o.Scale.X*pixelsPerMeter);var h=Math.Max(6,o.Scale.Y*pixelsPerMeter);yield return new(o.Id,o.Name,originX+o.Position.X*pixelsPerMeter-w/2,originY-o.Position.Y*pixelsPerMeter-h/2,w,h,o.Type);}} }
