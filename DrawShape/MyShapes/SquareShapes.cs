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
    class SquareShapes : MyShapes
    {
        public SquareShapes()
            : base()
        {
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            SquareShapes squareShape = new SquareShapes();
            squareShape.StartPoint = startPoint;
            squareShape.EndPoint = endPoint;
            squareShape.StrokeA = strokeColor.Color.A;
            squareShape.StrokeR = strokeColor.Color.R;
            squareShape.StrokeG = strokeColor.Color.G;
            squareShape.StrokeB = strokeColor.Color.B;
            squareShape.FillA = fillColor.Color.A;
            squareShape.FillR = fillColor.Color.R;
            squareShape.FillG = fillColor.Color.G;
            squareShape.FillB = fillColor.Color.B;
            squareShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);
            return squareShape;
        }

        public override UIElement CreateShape(MyShapes myShapeObject)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();
            SolidColorBrush fillColor = new SolidColorBrush();
            Rectangle myRect = new Rectangle();
            double width = Math.Abs(myShapeObject.StartPoint.X - myShapeObject.EndPoint.X);
            double height = Math.Abs(myShapeObject.StartPoint.Y - myShapeObject.EndPoint.Y);
            double left = Math.Min(myShapeObject.StartPoint.X, myShapeObject.EndPoint.X);
            double top = Math.Min(myShapeObject.StartPoint.Y, myShapeObject.EndPoint.Y);
            double side = Math.Max(width, height);
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myRect.Stroke = strokeColor;
            fillColor.Color = Color.FromArgb(FillA, FillR, FillG, FillB);
            myRect.Fill = fillColor;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Top;
            myRect.Height = side;
            myRect.Width = side;
            myRect.StrokeThickness = myShapeObject.StrokeThickness;
            Canvas.SetLeft(myRect, left);
            Canvas.SetTop(myRect, top);
            return myRect;
        }
    }
}
