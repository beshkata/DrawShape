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
    class PolygonShapes : MyShapes
    {
        public List<Point> polygonPointList = new List<Point>();
        
        public PolygonShapes()
            : base()
        {
        }

        public MyShapes CreatePolygonObject(List<Point> polygonPoints, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            PolygonShapes polygonShape = new PolygonShapes();

            for (int i = 0; i < polygonPoints.Count; i++)
            {
                polygonShape.polygonPointList.Add(polygonPoints[i]);
            }
            polygonShape.StrokeA = strokeColor.Color.A;
            polygonShape.StrokeR = strokeColor.Color.R;
            polygonShape.StrokeG = strokeColor.Color.G;
            polygonShape.StrokeB = strokeColor.Color.B;
            polygonShape.FillA = fillColor.Color.A;
            polygonShape.FillR = fillColor.Color.R;
            polygonShape.FillG = fillColor.Color.G;
            polygonShape.FillB = fillColor.Color.B;
            polygonShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);

            return polygonShape;
        }

        public UIElement CreatePolygonShape(PolygonShapes polygonShape)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();
            SolidColorBrush fillColor = new SolidColorBrush();
            Polygon myPolygon = new Polygon();
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myPolygon.Stroke = strokeColor;
            fillColor.Color = Color.FromArgb(FillA, FillR, FillG, FillB);
            myPolygon.Fill = fillColor;
            myPolygon.StrokeThickness = polygonShape.StrokeThickness;

            PointCollection myPointCollection = new PointCollection();
            for (int i = 0; i < polygonShape.polygonPointList.Count; i++)
            {
                myPointCollection.Add(polygonShape.polygonPointList[i]);
            }
            myPolygon.Points = myPointCollection;
            return myPolygon;
        }

        public static PolygonShapes MovePolygon(double subtractionX, double subtractionY, PolygonShapes polygonShape)
        {
            for (int i = 0; i < polygonShape.polygonPointList.Count; i++)
            {
                polygonShape.polygonPointList[i] = new Point(polygonShape.polygonPointList[i].X + subtractionX, polygonShape.polygonPointList[i].Y + subtractionY);
            }
            return polygonShape;
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            throw new NotImplementedException();
        }

        public override UIElement CreateShape(MyShapes myShapeObject)
        {
            throw new NotImplementedException();
        }
    }
}
