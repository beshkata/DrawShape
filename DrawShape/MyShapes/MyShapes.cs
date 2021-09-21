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
    abstract class MyShapes
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public byte StrokeA { get; set; }
        public byte StrokeR { get; set; }
        public byte StrokeG { get; set; }
        public byte StrokeB { get; set; }
        public int StrokeThickness { get; set; }
        public byte FillA { get; set; }
        public byte FillR { get; set; }
        public byte FillG { get; set; }
        public byte FillB { get; set; }

        public MyShapes()
        {
        }

        abstract public MyShapes CreateMyShapesObject(Point startPoint,
                                                      Point endPoint,
                                                      SolidColorBrush strokeColor,
                                                      SolidColorBrush fillColor,
                                                      ComboBox strokeThickness);

        abstract public UIElement CreateShape(MyShapes myShapeObject);

        public void DrawMyShape(Canvas canvas, UIElement myUIE)
        {
            canvas.Children.Add(myUIE);
        }

        //draw circle on the selected point from PolyLine and Polygon shape
        public static void AddPointCircle(Ellipse ellipse, List<Ellipse> pointCircles, Canvas canvas)
        {
            pointCircles.Add(ellipse);
            canvas.Children.Add(pointCircles[pointCircles.Count - 1]);
        }

        //create a circle, which will be drawn on a PolyLine or Polygon point
        public static Ellipse AddPolyPoints(Point ellipseCenter)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = System.Windows.Media.Brushes.Black;
            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(ellipse, ellipseCenter.X - 2);
            Canvas.SetTop(ellipse, ellipseCenter.Y - 2);
            ellipse.Width = 4;
            ellipse.Height = 4;
            return ellipse;
        }

        //Find which element has been chosen
        public static int FindSelectedObjectIndex(List<MyShapes> myShapes, UIElement selectedElement, int index)
        {

            for (int i = 0; i < myShapes.Count; i++)
            {
                if (selectedElement.ToString() == "System.Windows.Shapes.Line" &&
                    myShapes[i].ToString() == "DrawShape.LineShapes")
                {
                    Line selectedLine = new Line();
                    selectedLine = (Line)selectedElement;
                    if (myShapes[i].StartPoint.X == selectedLine.X1 &&
                        myShapes[i].StartPoint.Y == selectedLine.Y1 &&
                        myShapes[i].EndPoint.X == selectedLine.X2 &&
                        myShapes[i].EndPoint.Y == selectedLine.Y2 &&
                        myShapes[i].StrokeThickness == selectedLine.StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }
                else if (selectedElement.ToString() == "System.Windows.Shapes.Polyline" &&
                    myShapes[i].ToString() == "DrawShape.PolyLineShapes")
                {
                    bool isPolylinesEqual = true;
                    Polyline selectedPolyline = new Polyline();
                    selectedPolyline = (Polyline)selectedElement;
                    PolyLineShapes polilyneShape = new PolyLineShapes();
                    polilyneShape = (PolyLineShapes)myShapes[i];
                    for (int j = 0; j < selectedPolyline.Points.Count; j++)
                    {
                        if (selectedPolyline.Points[j] == polilyneShape.polyLinePointList[j])
                        {
                            continue;
                        }
                        else
                        {
                            isPolylinesEqual = false;
                            break;
                        }
                    }
                    if (isPolylinesEqual == true &&
                        selectedPolyline.StrokeThickness == myShapes[i].StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }

                else if (selectedElement.ToString() == "System.Windows.Shapes.Rectangle" &&
                         (myShapes[i].ToString() == "DrawShape.SquareShapes" || myShapes[i].ToString() == "DrawShape.RectangleShapes"))
                {
                    Rectangle selectedRectangle = new Rectangle();
                    selectedRectangle = (Rectangle)selectedElement;
                    Rectangle newRect = (Rectangle)myShapes[i].CreateShape(myShapes[i]);
                    if (Canvas.GetLeft(selectedRectangle) == Canvas.GetLeft(newRect) &&
                        Canvas.GetTop(selectedRectangle) == Canvas.GetTop(newRect) &&
                        selectedRectangle.Width == newRect.Width &&
                        selectedRectangle.Height == newRect.Height &&
                        selectedRectangle.StrokeThickness == newRect.StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }
                else if (selectedElement.ToString() == "System.Windows.Shapes.Ellipse" &&
                    (myShapes[i].ToString() == "DrawShape.CircleShapes" || myShapes[i].ToString() == "DrawShape.EllipseShapes"))
                {
                    Ellipse selectedEllipse = new Ellipse();
                    selectedEllipse = (Ellipse)selectedElement;
                    Ellipse newEllipse = (Ellipse)myShapes[i].CreateShape(myShapes[i]);
                    if (Canvas.GetLeft(selectedEllipse) == Canvas.GetLeft(newEllipse) &&
                        Canvas.GetTop(selectedEllipse) == Canvas.GetTop(newEllipse) &&
                        selectedEllipse.Width == newEllipse.Width &&
                        selectedEllipse.Height == newEllipse.Height &&
                        selectedEllipse.StrokeThickness == newEllipse.StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }
                else if (selectedElement.ToString() == "System.Windows.Shapes.Path" &&
                         myShapes[i].ToString() == "DrawShape.ArcShapes")
                {
                    Path selectedPath = new Path();
                    selectedPath = (Path)selectedElement;
                    PathGeometry pg = new PathGeometry();
                    pg = (PathGeometry)selectedPath.Data;
                    PathFigureCollection pfc = new PathFigureCollection();
                    pfc = pg.Figures;
                    PathFigure pf = new PathFigure();
                    pf = pfc[0];
                    PathSegmentCollection psc = new PathSegmentCollection();
                    psc = pf.Segments;
                    ArcSegment arcS = new ArcSegment();
                    arcS = (ArcSegment)psc[0];

                    ArcShapes arcShape = new ArcShapes();
                    arcShape = (ArcShapes)myShapes[i];
                    if (arcShape.StartPoint == pf.StartPoint &&
                        arcShape.IsLarge == arcS.IsLargeArc &&
                        arcShape.EndPoint == arcS.Point &&
                        arcShape.Angle == arcS.RotationAngle &&
                        arcShape.ArcSize == arcS.Size &&
                        arcShape.StrokeThickness == selectedPath.StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }
                else if (selectedElement.ToString() == "System.Windows.Shapes.Polygon" &&
                    myShapes[i].ToString() == "DrawShape.PolygonShapes")
                {
                    bool isPolygonsEqual = true;
                    Polygon selectedPolygon = new Polygon();
                    selectedPolygon = (Polygon)selectedElement;
                    PolygonShapes poligonShape = new PolygonShapes();
                    poligonShape = (PolygonShapes)myShapes[i];
                    for (int j = 0; j < selectedPolygon.Points.Count; j++)
                    {
                        if (selectedPolygon.Points[j] == poligonShape.polygonPointList[j])
                        {
                            continue;
                        }
                        else
                        {
                            isPolygonsEqual = false;
                            break;
                        }
                    }
                    if (isPolygonsEqual == true &&
                        selectedPolygon.StrokeThickness == myShapes[i].StrokeThickness)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

    }
}
