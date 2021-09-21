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
    class EllipseShapes : MyShapes
    {
        public EllipseShapes()
            : base()
        {
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            EllipseShapes ellipseShape = new EllipseShapes();
            ellipseShape.StartPoint = startPoint;
            ellipseShape.EndPoint = endPoint;
            ellipseShape.StrokeA = strokeColor.Color.A;
            ellipseShape.StrokeR = strokeColor.Color.R;
            ellipseShape.StrokeG = strokeColor.Color.G;
            ellipseShape.StrokeB = strokeColor.Color.B;
            ellipseShape.FillA = fillColor.Color.A;
            ellipseShape.FillR = fillColor.Color.R;
            ellipseShape.FillG = fillColor.Color.G;
            ellipseShape.FillB = fillColor.Color.B;
            ellipseShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);

            return ellipseShape;
        }

        public override UIElement CreateShape(MyShapes myShapeObject)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();
            SolidColorBrush fillColor = new SolidColorBrush();

            Ellipse myEllipse = new Ellipse();

            double width = Math.Abs(myShapeObject.StartPoint.X - myShapeObject.EndPoint.X);
            double height = Math.Abs(myShapeObject.StartPoint.Y - myShapeObject.EndPoint.Y);
            double left = Math.Min(myShapeObject.StartPoint.X, myShapeObject.EndPoint.X);
            double top = Math.Min(myShapeObject.StartPoint.Y, myShapeObject.EndPoint.Y);
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myEllipse.Stroke = strokeColor;
            fillColor.Color = Color.FromArgb(FillA, FillR, FillG, FillB);
            myEllipse.Fill = fillColor;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Left;
            myEllipse.VerticalAlignment = VerticalAlignment.Top;
            myEllipse.Height = height;
            myEllipse.Width = width;
            myEllipse.StrokeThickness = myShapeObject.StrokeThickness;
            Canvas.SetLeft(myEllipse, left);
            Canvas.SetTop(myEllipse, top);

            return myEllipse;
        }
    }
}
