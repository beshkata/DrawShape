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
    class LineShapes : MyShapes
    {
        public LineShapes()
            : base()
        {
        }

        override public UIElement CreateShape(MyShapes myShapeObject)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();
            Line line = new Line();
            line.X1 = myShapeObject.StartPoint.X;
            line.Y1 = myShapeObject.StartPoint.Y;
            line.X2 = myShapeObject.EndPoint.X;
            line.Y2 = myShapeObject.EndPoint.Y;
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            line.Stroke = strokeColor;
            line.StrokeThickness = StrokeThickness;
            return line;
        }

        public override MyShapes CreateMyShapesObject(Point startPoint, Point endPoint, SolidColorBrush strokeColor, SolidColorBrush fillColor, ComboBox strokeThickness)
        {
            LineShapes lineShape = new LineShapes();
            lineShape.StartPoint = startPoint;
            lineShape.EndPoint = endPoint;
            lineShape.StrokeA = strokeColor.Color.A;
            lineShape.StrokeR = strokeColor.Color.R;
            lineShape.StrokeG = strokeColor.Color.G;
            lineShape.StrokeB = strokeColor.Color.B;
            lineShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);
            return lineShape;
        }
    }
}
