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
    class CircleShapes : MyShapes
    {
        public CircleShapes()
            : base()
        {
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            CircleShapes circleShape = new CircleShapes();
            circleShape.StartPoint = startPoint;
            circleShape.EndPoint = endPoint;
            circleShape.StrokeA = strokeColor.Color.A;
            circleShape.StrokeR = strokeColor.Color.R;
            circleShape.StrokeG = strokeColor.Color.G;
            circleShape.StrokeB = strokeColor.Color.B;
            circleShape.FillA = fillColor.Color.A;
            circleShape.FillR = fillColor.Color.R;
            circleShape.FillG = fillColor.Color.G;
            circleShape.FillB = fillColor.Color.B;
            circleShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);

            return circleShape;
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
            double side = Math.Max(width, height);
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myEllipse.Stroke = strokeColor;
            fillColor.Color = Color.FromArgb(FillA, FillR, FillG, FillB);
            myEllipse.Fill = fillColor;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Left;
            myEllipse.VerticalAlignment = VerticalAlignment.Top;
            myEllipse.Height = side;
            myEllipse.Width = side;
            myEllipse.StrokeThickness = myShapeObject.StrokeThickness;
            Canvas.SetLeft(myEllipse, left);
            Canvas.SetTop(myEllipse, top);

            return myEllipse;
        }
    }
}
