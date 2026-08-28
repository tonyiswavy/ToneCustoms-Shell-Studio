using System.Windows.Media;using System.Windows.Media.Media3D;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Geometry;
public sealed class StairMeshBuilder
{
    public MeshGeometry3D Build(SceneObject stairs)
    {
        var mesh=new MeshGeometry3D();var steps=Math.Clamp((int)MathF.Round(Math.Max(1,stairs.Scale.Z/.18f)),1,64);var width=Math.Max(.25f,stairs.Scale.X);var run=Math.Max(.25f,stairs.Scale.Y);var height=Math.Max(.1f,stairs.Scale.Z);var tread=run/steps;var rise=height/steps;
        for(var i=0;i<steps;i++){var d=tread*(i+1);var h=rise*(i+1);AddBox(mesh,-width/2,width/2,-run/2,-run/2+d,0,h);}return mesh;
    }
    static void AddBox(MeshGeometry3D m,float x0,float x1,float y0,float y1,float z0,float z1)
    {
        var b=m.Positions.Count;m.Positions.Add(new Point3D(x0,y0,z0));m.Positions.Add(new Point3D(x1,y0,z0));m.Positions.Add(new Point3D(x1,y1,z0));m.Positions.Add(new Point3D(x0,y1,z0));m.Positions.Add(new Point3D(x0,y0,z1));m.Positions.Add(new Point3D(x1,y0,z1));m.Positions.Add(new Point3D(x1,y1,z1));m.Positions.Add(new Point3D(x0,y1,z1));foreach(var i in new[]{0,2,1,0,3,2,4,5,6,4,6,7,0,1,5,0,5,4,1,2,6,1,6,5,2,3,7,2,7,6,3,0,4,3,4,7})m.TriangleIndices.Add(b+i);
    }
}
