using System.Windows.Controls;using System.Windows.Media;using System.Windows.Shapes;
namespace ToneCustoms.ShellStudio.Editor;
public static class GridRenderer { public static void Draw(Canvas c,double step=35){c.Children.Clear();for(double x=0;x<c.ActualWidth;x+=step)c.Children.Add(new Line{X1=x,Y1=0,X2=x,Y2=c.ActualHeight,Stroke=new SolidColorBrush(Color.FromRgb(30,30,38)),StrokeThickness=1});for(double y=0;y<c.ActualHeight;y+=step)c.Children.Add(new Line{X1=0,Y1=y,X2=c.ActualWidth,Y2=y,Stroke=new SolidColorBrush(Color.FromRgb(30,30,38)),StrokeThickness=1});} }
