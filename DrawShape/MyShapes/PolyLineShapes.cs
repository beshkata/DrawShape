using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawShape
{
    [Serializable]
    class PolyLineShapes : MyShapes
    {
        public List<Point> polyLinePointList = new List<Point>();
        
        public PolyLineShapes()
            : base()
        {
        }

        public MyShapes CreatePolyLineObject(List<Point> polyLinePoints, SolidColorBrush strokeColor, ComboBox strokeThickness)
        {
            PolyLineShapes polyLineShape = new PolyLineShapes();

            for (int i = 0; i < polyLinePoints.Count; i++)
            {
                polyLineShape.polyLinePointList.Add(polyLinePoints[i]);
            }
            polyLineShape.StrokeA = strokeColor.Color.A;
            polyLineShape.StrokeR = strokeColor.Color.R;
            polyLineShape.StrokeG = strokeColor.Color.G;
            polyLineShape.StrokeB = strokeColor.Color.B;
            polyLineShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);

            return polyLineShape;
        }

        public UIElement CreatePolyLineShape(PolyLineShapes polyLineShape)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();
            Polyline myPolyline = new Polyline();
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myPolyline.Stroke = strokeColor;
            myPolyline.StrokeThickness = polyLineShape.StrokeThickness;
            myPolyline.FillRule = FillRule.EvenOdd;

            PointCollection myPointCollection = new PointCollection();
            for (int i = 0; i < polyLineShape.polyLinePointList.Count; i++)
            {
                myPointCollection.Add(polyLineShape.polyLinePointList[i]);
            }
            myPolyline.Points = myPointCollection;
            return myPolyline;
        }

        public static PolyLineShapes MovePolyLine(double subtractionX, double subtractionY, PolyLineShapes polylineShape)
        {
            for (int i = 0; i < polylineShape.polyLinePointList.Count; i++)
            {
                polylineShape.polyLinePointList[i] = new Point(polylineShape.polyLinePointList[i].X + subtractionX, polylineShape.polyLinePointList[i].Y + subtractionY);
            }
            return polylineShape;
        }


        public override UIElement CreateShape(MyShapes myShapeObject)
        {
            throw new NotImplementedException();
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            throw new NotImplementedException();
        }

    }
}
