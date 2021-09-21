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
    class RectangleShapes : MyShapes
    {
        public RectangleShapes()
            : base()
        {
        }

        public override MyShapes CreateMyShapesObject(System.Windows.Point startPoint, System.Windows.Point endPoint, System.Windows.Media.SolidColorBrush strokeColor, System.Windows.Media.SolidColorBrush fillColor, System.Windows.Controls.ComboBox strokeThickness)
        {
            RectangleShapes rectangleShape = new RectangleShapes();
            rectangleShape.StartPoint = startPoint;
            rectangleShape.EndPoint = endPoint;
            rectangleShape.StrokeA = strokeColor.Color.A;
            rectangleShape.StrokeR = strokeColor.Color.R;
            rectangleShape.StrokeG = strokeColor.Color.G;
            rectangleShape.StrokeB = strokeColor.Color.B;
            rectangleShape.FillA = fillColor.Color.A;
            rectangleShape.FillR = fillColor.Color.R;
            rectangleShape.FillG = fillColor.Color.G;
            rectangleShape.FillB = fillColor.Color.B;
            rectangleShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);
            return rectangleShape;
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
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            myRect.Stroke = strokeColor;
            fillColor.Color = Color.FromArgb(FillA, FillR, FillG, FillB);
            myRect.Fill = fillColor;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Top;
            myRect.Height = height;
            myRect.Width = width;
            myRect.StrokeThickness = myShapeObject.StrokeThickness;
            Canvas.SetLeft(myRect, left);
            Canvas.SetTop(myRect, top);
            return myRect;
        }
    }
}
