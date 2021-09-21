using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DrawShape
{
    [Serializable]
    class SockTemp
    {
        public static double distanceFromBorder = 2*(96/2.54);
        public static double distanceFromShape = 10;

        public double SizeA { get; set; }
        public double SizeB { get; set; }
        public double SizeC { get; set; }
        public double SizeD { get; set; }
        public double SizeE { get; set; }

        public SockTemp()
        {
        }

        public SockTemp(double [] sockSizes)
        {
            this.SizeA = sockSizes[0];
            this.SizeB = sockSizes[1];
            this.SizeC = sockSizes[2];
            this.SizeD = sockSizes[3];
            this.SizeE = sockSizes[4];
        }

        public void DrawSockTemp(Canvas canvas)
        {
            double distanceDraw = SizeA + distanceFromBorder * 2;

            if (SizeB + 150 > canvas.Height)
            {
                canvas.Height = SizeB + 150;
            }

            if (distanceFromBorder+SizeA > canvas.Width)
            {
                canvas.Width = distanceDraw * 2;
            }

            SolidColorBrush strokeColor = new SolidColorBrush(Colors.Black);
            int strokeThickness = 2;

            Polyline myPolyline = new Polyline();
            myPolyline.Stroke = strokeColor;
            myPolyline.StrokeThickness = strokeThickness;
            myPolyline.FillRule = FillRule.EvenOdd;

            Polygon myFirstPolygon = new Polygon();
            myFirstPolygon.Stroke = strokeColor;
            myFirstPolygon.StrokeThickness = strokeThickness;

            //first projection
            PointCollection myPolylinePointCollection = new PointCollection();
            myPolylinePointCollection.Add(new Point(distanceFromBorder, (SizeB - SizeE) + distanceFromBorder));
            myPolylinePointCollection.Add(new Point(((SizeA - SizeD) / 2) + distanceFromBorder, (SizeB - SizeC) + distanceFromBorder));
            myPolylinePointCollection.Add(new Point(SizeA - ((SizeA - SizeD) / 2) + distanceFromBorder, (SizeB - SizeC) + distanceFromBorder));
            myPolylinePointCollection.Add(new Point(SizeA + distanceFromBorder, (SizeB - SizeE) + distanceFromBorder));
            myPolyline.Points = myPolylinePointCollection;

            PointCollection myFirstPointCollection = new PointCollection();
            myFirstPointCollection.Add(new Point(distanceFromBorder, distanceFromBorder));
            myFirstPointCollection.Add(new Point(SizeA + distanceFromBorder, distanceFromBorder));
            myFirstPointCollection.Add(new Point(SizeA + distanceFromBorder, SizeB - (SizeC - SizeE) + distanceFromBorder));
            myFirstPointCollection.Add(new Point(SizeA - ((SizeA - SizeD) / 2) + distanceFromBorder, SizeB + distanceFromBorder));
            myFirstPointCollection.Add(new Point((SizeA - SizeD) / 2 + distanceFromBorder, SizeB + distanceFromBorder));
            myFirstPointCollection.Add(new Point(distanceFromBorder, SizeB - (SizeC - SizeE) + distanceFromBorder));
            myFirstPolygon.Points = myFirstPointCollection;

            //second projection
            Polygon mySecondPolygon = new Polygon();
            mySecondPolygon.Stroke = strokeColor;
            mySecondPolygon.StrokeThickness = strokeThickness;

            PointCollection mySecondPointCollection = new PointCollection();
            mySecondPointCollection.Add(new Point(distanceDraw, distanceFromBorder));
            mySecondPointCollection.Add(new Point(distanceDraw + SizeA, distanceFromBorder));
            mySecondPointCollection.Add(new Point(distanceDraw + SizeA, SizeB - (SizeC - SizeE) + distanceFromBorder));
            mySecondPointCollection.Add(new Point(SizeA - ((SizeA - SizeD) / 2) + distanceDraw, SizeB + distanceFromBorder));
            mySecondPointCollection.Add(new Point((SizeA - SizeD) / 2 + distanceDraw, SizeB + distanceFromBorder));
            mySecondPointCollection.Add(new Point(distanceDraw, SizeB - (SizeC - SizeE) + distanceFromBorder));
            mySecondPolygon.Points = mySecondPointCollection;

            Line myLine = new Line();
            myLine.Stroke = strokeColor;
            myLine.StrokeThickness = strokeThickness;
            myLine.X1 = distanceDraw;
            myLine.Y1 = (SizeB - SizeE) + distanceFromBorder;
            myLine.X2 = distanceDraw + SizeA;
            myLine.Y2 = (SizeB - SizeE) + distanceFromBorder;

            canvas.Children.Add(myFirstPolygon);
            MainWindow.drawedSockTemp.Add(myFirstPolygon);
            
            canvas.Children.Add(myPolyline);
            MainWindow.drawedSockTemp.Add(myPolyline);
            canvas.Children.Add(mySecondPolygon);
            MainWindow.drawedSockTemp.Add(mySecondPolygon);
            canvas.Children.Add(myLine);
            MainWindow.drawedSockTemp.Add(myLine);
        }

        public void DrawSizes(Canvas canvas)
        {
            canvas.Children.Add(DrawLine(distanceFromBorder - distanceFromShape * 2, distanceFromBorder, distanceFromBorder, distanceFromBorder));
            canvas.Children.Add(DrawLine(distanceFromBorder - distanceFromShape * 2, (SizeB - SizeC) + distanceFromBorder, distanceFromBorder, (SizeB - SizeC) + distanceFromBorder));
            canvas.Children.Add(DrawLine(distanceFromBorder - distanceFromShape * 2, (SizeB - SizeE) + distanceFromBorder, distanceFromBorder, (SizeB - SizeE) + distanceFromBorder));
            canvas.Children.Add(DrawLine(distanceFromBorder - distanceFromShape * 2, (SizeB - (SizeC - SizeE)) + distanceFromBorder, distanceFromBorder, (SizeB - (SizeC - SizeE)) + distanceFromBorder));
            canvas.Children.Add(DrawLine(distanceFromBorder - distanceFromShape * 2, SizeB + distanceFromBorder, ((SizeA - SizeD) / 2) + distanceFromBorder, SizeB + distanceFromBorder));
            canvas.Children.Add(DrawLine((distanceFromBorder * 2) + (SizeA * 2), distanceFromBorder, (distanceFromBorder * 2) + (SizeA * 2) + distanceFromShape * 6, distanceFromBorder));
            canvas.Children.Add(DrawLine((distanceFromBorder * 2) + (SizeA * 2), (SizeB - SizeE) + distanceFromBorder, (distanceFromBorder * 2) + (SizeA * 2) + distanceFromShape * 2, (SizeB - SizeE) + distanceFromBorder));
            canvas.Children.Add(DrawLine(((distanceFromBorder * 2) + (SizeA * 2)) - ((SizeA - SizeD) / 2), SizeB + distanceFromBorder, (distanceFromBorder * 2) + (SizeA * 2) + distanceFromShape * 6, SizeB + distanceFromBorder));
            canvas.Children.Add(DrawLine(distanceFromBorder, distanceFromBorder - distanceFromShape * 2, distanceFromBorder, distanceFromBorder));
            canvas.Children.Add(DrawLine(SizeA + distanceFromBorder, distanceFromBorder - distanceFromShape * 2, SizeA + distanceFromBorder, distanceFromBorder));
            canvas.Children.Add(DrawLine(((SizeA - SizeD) / 2) + distanceFromBorder, distanceFromBorder + SizeB, ((SizeA - SizeD) / 2) + distanceFromBorder, distanceFromBorder + SizeB + distanceFromShape * 4));
            canvas.Children.Add(DrawLine(((SizeA - (SizeA - SizeD) / 2)) + distanceFromBorder, distanceFromBorder + SizeB, ((SizeA - (SizeA - SizeD) / 2)) + distanceFromBorder, distanceFromBorder + SizeB + distanceFromShape * 4));
            canvas.Children.Add(DrawLine(distanceFromBorder + SizeA, (SizeB - (SizeC - SizeE)) + distanceFromBorder, distanceFromBorder + SizeA, SizeB + distanceFromBorder + distanceFromShape * 4));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder - distanceFromShape, distanceFromBorder, distanceFromBorder - distanceFromShape, (SizeB - SizeC) + distanceFromBorder));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder - distanceFromShape, distanceFromBorder, distanceFromBorder - distanceFromShape, (SizeB - SizeC) + distanceFromBorder));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeC), distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeE)));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeC), distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeE)));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeE), distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - (SizeC - SizeE))));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - SizeE), distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - (SizeC - SizeE))));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - (SizeC - SizeE)), distanceFromBorder - distanceFromShape, distanceFromBorder + SizeB));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder - distanceFromShape, distanceFromBorder + (SizeB - (SizeC - SizeE)), distanceFromBorder - distanceFromShape, distanceFromBorder + SizeB));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder, distanceFromBorder - distanceFromShape, distanceFromBorder + SizeA, distanceFromBorder - distanceFromShape));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder, distanceFromBorder - distanceFromShape, distanceFromBorder + SizeA, distanceFromBorder - distanceFromShape));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder + ((SizeA - SizeD) / 2), distanceFromBorder + distanceFromShape * 3 + SizeB, distanceFromBorder + (SizeA - ((SizeA - SizeD) / 2)), distanceFromBorder + distanceFromShape * 3 + SizeB));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder + ((SizeA - SizeD) / 2), distanceFromBorder + distanceFromShape * 3 + SizeB, distanceFromBorder + (SizeA - ((SizeA - SizeD) / 2)), distanceFromBorder + distanceFromShape * 3 + SizeB));

            canvas.Children.Add(DrawArrowLine(distanceFromBorder + (SizeA - ((SizeA - SizeD) / 2)), distanceFromBorder + distanceFromShape * 3 + SizeB, distanceFromBorder + SizeA, distanceFromBorder + distanceFromShape * 3 + SizeB));
            canvas.Children.Add(DrawTextBlockSize(distanceFromBorder + (SizeA - ((SizeA - SizeD) / 2)), distanceFromBorder + distanceFromShape * 3 + SizeB, distanceFromBorder + SizeA, distanceFromBorder + distanceFromShape * 3 + SizeB));

            canvas.Children.Add(DrawArrowLine(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, (SizeB - SizeE) + distanceFromBorder));
            canvas.Children.Add(DrawTextBlockSize(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, (SizeB - SizeE) + distanceFromBorder));

            canvas.Children.Add(DrawArrowLine(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, (SizeB - SizeE) + distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, SizeB + distanceFromBorder));
            canvas.Children.Add(DrawTextBlockSize(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, (SizeB - SizeE) + distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape, SizeB + distanceFromBorder));

            canvas.Children.Add(DrawArrowLine(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape * 5, distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape * 5, SizeB + distanceFromBorder));
            canvas.Children.Add(DrawTextBlockSize(SizeA * 2 + distanceFromBorder * 2 + distanceFromShape * 5, distanceFromBorder, SizeA * 2 + distanceFromBorder * 2 + distanceFromShape * 5, SizeB + distanceFromBorder));

        }

        Line DrawLine(double x1, double y1, double x2, double y2)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 0.5;

            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            MainWindow.drawedSockSizes.Add(line);

            return line;
        }

        Path DrawArrowLine(double x1, double y1, double x2, double y2)
        {
            PathFigure pathFigure = new PathFigure();

            PathGeometry pathGeometry = new PathGeometry();

            Path arrowPath = new Path();
            arrowPath.Stroke = Brushes.Black;
            arrowPath.StrokeThickness = 0.5;
            arrowPath.Fill = new SolidColorBrush(Colors.Black);


            if (x1 == x2)
            {
                pathFigure.StartPoint = new Point(x1, y1 + 2);

                LineSegment lineSeg = new LineSegment();
                lineSeg.Point = new Point(x2, y2 - 2);
                lineSeg.IsStroked = true;
                pathFigure.Segments.Add(lineSeg);

                PolyLineSegment polyLineSegStart = new PolyLineSegment();
                Point[] pointCollection = new Point[] { new Point(x1, y1 + 1), new Point(x1 - 1, y1 + 4), new Point(x1, y1 + 3), new Point(x1 + 1, y1 + 4), new Point(x1, y1 + 1) };
                polyLineSegStart.Points = new PointCollection(pointCollection);

                pathFigure.Segments.Add(polyLineSegStart);

                PolyLineSegment polyLineSegEnd = new PolyLineSegment();
                Point[] pointCollectionEnd = new Point[] { new Point(x2, y2 - 1), new Point(x2 - 1, y2 - 4), new Point(x2, y2 - 3), new Point(x2 + 1, y2 - 4), new Point(x2, y2 - 1) };
                polyLineSegEnd.Points = new PointCollection(pointCollectionEnd);

                pathFigure.Segments.Add(polyLineSegEnd);

            }
            else
            {
                pathFigure.StartPoint = new Point(x1 + 2, y1);

                LineSegment lineSeg = new LineSegment();
                lineSeg.Point = new Point(x2 - 2, y2);
                lineSeg.IsStroked = true;
                pathFigure.Segments.Add(lineSeg);

                PolyLineSegment polyLineSegStart = new PolyLineSegment();
                Point[] pointCollection = new Point[] { new Point(x1 + 1, y1), new Point(x1 + 4, y1 + 1), new Point(x1 + 3, y1), new Point(x1 + 4, y1 - 1), new Point(x1 + 1, y1) };
                polyLineSegStart.Points = new PointCollection(pointCollection);

                pathFigure.Segments.Add(polyLineSegStart);

                PolyLineSegment polyLineSegEnd = new PolyLineSegment();
                Point[] pointCollectionEnd = new Point[] { new Point(x2 - 1, y2), new Point(x2 - 4, y2 - 1), new Point(x2 - 3, y2), new Point(x2 -4, y2 +1), new Point(x2 - 1, y2) };
                polyLineSegEnd.Points = new PointCollection(pointCollectionEnd);

                pathFigure.Segments.Add(polyLineSegEnd);

            }

            pathGeometry.Figures.Add(pathFigure);

            arrowPath.Data = pathGeometry;
            arrowPath.StrokeStartLineCap = PenLineCap.Triangle;
            arrowPath.StrokeEndLineCap = PenLineCap.Triangle;

            MainWindow.drawedSockSizes.Add(arrowPath);
            
            return arrowPath;
        }

        TextBlock DrawTextBlockSize(double x1, double y1, double x2, double y2)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 12;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Height = 20;


            if (x1 == x2)
            {
                textBlock.Width = y2 - y1;

                if (x1 < SizeA*2+distanceFromBorder*2)
                {

                    textBlock.Margin = new Thickness(x1 - 15, y2, 0, 0);
                    textBlock.Text = (textBlock.Width / (96/2.54)).ToString();
                    RotateTransform rt = new RotateTransform(270);
                    textBlock.RenderTransform = rt;

                }
                else
                {
                    textBlock.Margin = new Thickness(x1 + 15, y1, 0, 0);
                    textBlock.Text = (textBlock.Width / (96 / 2.54)).ToString();
                    RotateTransform rt = new RotateTransform(90);
                    textBlock.RenderTransform = rt;
                }
            }
            else
            {
                textBlock.Width = x2 - x1;

                textBlock.Margin = new Thickness(x1, y2 - 15, 0, 0);
                textBlock.Text = (textBlock.Width / (96 / 2.54)).ToString();
            }

            MainWindow.drawedSockSizes.Add(textBlock);

            return textBlock;
        }

        public void RemoveDrawedTemp(Canvas canvas)
        {
            for (int i = 0; i < MainWindow.drawedSockTemp.Count; i++)
            {
                canvas.Children.Remove(MainWindow.drawedSockTemp[i]);
                MainWindow.drawedSockTemp[i] = null;
            }
        }

        public void RemoveDrawedSizes(Canvas canvas)
        {
            for (int i = 0; i < MainWindow.drawedSockSizes.Count; i++)
            {
                canvas.Children.Remove(MainWindow.drawedSockSizes[i]);
                MainWindow.drawedSockSizes[i] = null;
            }
        }
    }
}
