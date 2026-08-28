using System.Windows.Media;using System.Windows.Media.Media3D;using ToneCustoms.ShellStudio.Core;
namespace ToneCustoms.ShellStudio.Geometry;
public enum StairLayout{Straight,LShape,UShape}
public sealed class StairMeshBuilder
{
 public MeshGeometry3D Build(SceneObject stairs)=>Build(stairs,StairLayout.Straight);
 public MeshGeometry3D Build(SceneObject stairs,StairLayout layout)
 {
  var mesh=new MeshGeometry3D();var width=Math.Max(.25f,stairs.Scale.X);var run=Math.Max(.25f,stairs.Scale.Y);var height=Math.Max(.1f,stairs.Scale.Z);var steps=Math.Clamp((int)MathF.Round(height/.18f),1,64);
  if(layout==StairLayout.Straight){AddFlight(mesh,-width/2,width/2,-run/2,run/2,0,height,steps);return mesh;}
  var firstSteps=Math.Max(1,steps/2);var secondSteps=Math.Max(1,steps-firstSteps);var landingHeight=height*firstSteps/steps;var landing=Math.Max(width,.8f);var flightRun=Math.Max(.25f,(run-landing)/2);
  AddFlight(mesh,-width/2,width/2,-run/2,-run/2+flightRun,0,landingHeight,firstSteps);AddBox(mesh,-width/2,width/2,-run/2+flightRun,-run/2+flightRun+landing,0,landingHeight);
  if(layout==StairLayout.LShape){AddTurnedFlight(mesh,width/2,width/2+flightRun,-run/2+flightRun,-run/2+flightRun+width,landingHeight,height,secondSteps,false);}else{AddFlightReverse(mesh,-width/2,width/2,-run/2+flightRun+landing,-run/2+flightRun+landing+flightRun,landingHeight,height,secondSteps);}return mesh;
 }
 static void AddFlight(MeshGeometry3D mesh,float x0,float x1,float y0,float y1,float z0,float z1,int steps){var tread=(y1-y0)/steps;var rise=(z1-z0)/steps;for(var i=0;i<steps;i++)AddBox(mesh,x0,x1,y0,y0+tread*(i+1),z0,z0+rise*(i+1));}
 static void AddFlightReverse(MeshGeometry3D mesh,float x0,float x1,float y0,float y1,float z0,float z1,int steps){var tread=(y1-y0)/steps;var rise=(z1-z0)/steps;for(var i=0;i<steps;i++)AddBox(mesh,x0,x1,y1-tread*(i+1),y1,z0,z0+rise*(i+1));}
 static void AddTurnedFlight(MeshGeometry3D mesh,float x0,float x1,float y0,float y1,float z0,float z1,int steps,bool reverse){var tread=(x1-x0)/steps;var rise=(z1-z0)/steps;for(var i=0;i<steps;i++){var a=reverse?x1-tread*(i+1):x0;var b=reverse?x1:x0+tread*(i+1);AddBox(mesh,a,b,y0,y1,z0,z0+rise*(i+1));}}
 static void AddBox(MeshGeometry3D m,float x0,float x1,float y0,float y1,float z0,float z1){var b=m.Positions.Count;m.Positions.Add(new Point3D(x0,y0,z0));m.Positions.Add(new Point3D(x1,y0,z0));m.Positions.Add(new Point3D(x1,y1,z0));m.Positions.Add(new Point3D(x0,y1,z0));m.Positions.Add(new Point3D(x0,y0,z1));m.Positions.Add(new Point3D(x1,y0,z1));m.Positions.Add(new Point3D(x1,y1,z1));m.Positions.Add(new Point3D(x0,y1,z1));foreach(var i in new[]{0,2,1,0,3,2,4,5,6,4,6,7,0,1,5,0,5,4,1,2,6,1,6,5,2,3,7,2,7,6,3,0,4,3,4,7})m.TriangleIndices.Add(b+i);}
}
