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
    class ArcShapes : MyShapes
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public Size ArcSize = new Size();
        public double Angle { get; set; }
        public bool? IsLarge { get; set; }
        public string ArcSweepDirection { get; set; }

        public ArcShapes()
            : base()
        {
        }

        public MyShapes CreateArcShapeObject(Point startPoint, Point endPoint,
                                             SolidColorBrush strokeColor,
                                             double width, double height,
                                             double rotationAngle, string sweepDirection, 
                                             bool? isLarge, ComboBox strokeThickness)
        {
            ArcShapes arcShape = new ArcShapes();
            arcShape.StartPoint = startPoint;
            arcShape.EndPoint = endPoint;
            arcShape.StrokeA = strokeColor.Color.A;
            arcShape.StrokeR = strokeColor.Color.R;
            arcShape.StrokeG = strokeColor.Color.G;
            arcShape.StrokeB = strokeColor.Color.B;
            arcShape.StrokeThickness = Convert.ToInt32(strokeThickness.Text);
            arcShape.Width = width;
            arcShape.Height = height;
            arcShape.ArcSize = new Size(width, height);
            arcShape.Angle = rotationAngle;
            arcShape.ArcSweepDirection = sweepDirection;
            arcShape.IsLarge = isLarge;

            return arcShape;
        }

        public UIElement CreateArcShape(ArcShapes arcShape)
        {
            SolidColorBrush strokeColor = new SolidColorBrush();

            PathFigure pf = new PathFigure();
            pf.StartPoint = arcShape.StartPoint;

            Path path = new Path();
            strokeColor.Color = Color.FromArgb(StrokeA, StrokeR, StrokeG, StrokeB);
            path.Stroke = strokeColor;
            path.StrokeThickness = arcShape.StrokeThickness;

            ArcSegment arcS = new ArcSegment();

            arcS.Point = arcShape.EndPoint;
            arcS.Size = arcShape.ArcSize;
            arcS.RotationAngle = arcShape.Angle;
            if (arcShape.IsLarge == true)
            {
                arcS.IsLargeArc = true;
            }
            else
            {
                arcS.IsLargeArc = false;
            }

            if (arcShape.ArcSweepDirection == "Clockwise")
            {
                arcS.SweepDirection = SweepDirection.Clockwise;
            }
            else
            {
                arcS.SweepDirection = SweepDirection.Counterclockwise;
            }

            PathSegmentCollection psc = new PathSegmentCollection();
            psc.Add(arcS);

            pf.Segments = psc;

            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);

            PathGeometry pg = new PathGeometry();
            pg.Figures = pfc;

            path.Data = pg;
            return path;
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
