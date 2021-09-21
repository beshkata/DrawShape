using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Samples.CustomControls;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Windows.Forms;

namespace DrawShape
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Shape's stroke and fill colors
        private SolidColorBrush strokeBrushColor = new SolidColorBrush(Colors.Black);
        private SolidColorBrush fillBrushColor = new SolidColorBrush(Colors.Gray);

        //MyShapes variables
        List<MyShapes> myShapes = new List<MyShapes>();
        Point startPoint = new Point();
        Point endPoint = new Point();
        Point currentPoint = new Point();

        //MyShapes.PolyLineShapes, MyShapes.PolygonShapes
        List<Point> polyLinePoints = new List<Point>();//събира точките за създаване на PolyLineShapes и PolygonShapes обектите
        List<Ellipse> pointCircles = new List<Ellipse>();//събира кръгчетата, които маркират точките на PolyLineShapes и PolygonShapes обектите

        //select variables
        bool isSelected = false;
        UIElement selectedElement = new UIElement();
        

        //move variables
        List<UIElement> drawedUIE = new List<UIElement>();
        int index = 0; //поредния номер на обекта MyShapes в List<myShapes> , който е селектиран.
        bool isMoving = false;

        //Template array
        SockTemp[] sockTempArray = new SockTemp[1];
        public static List<UIElement> drawedSockSizes = new List<UIElement>();
        public static List<UIElement> drawedSockTemp = new List<UIElement>();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void btnFillColor_Click(object sender, RoutedEventArgs e)
        {
            //choosing shape's fill color by clicking Fill Color Button
            ColorPickerDialog colorPicker = new ColorPickerDialog();
            colorPicker.StartingColor = fillBrushColor.Color;
            colorPicker.Owner = this;
            rectFillColor.Fill = fillBrushColor;

            bool? dialogResult = colorPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                fillBrushColor = new SolidColorBrush(colorPicker.SelectedColor);
                rectFillColor.Fill = fillBrushColor;
            }
        }

        private void btnStrokeColor_Click(object sender, RoutedEventArgs e)
        {
            //choosing shape's stroke color by clicking Stroke Color Button
            ColorPickerDialog colorPicker = new ColorPickerDialog();
            colorPicker.StartingColor = strokeBrushColor.Color;
            colorPicker.Owner = this;
            rectStrokeColor.Fill = strokeBrushColor;

            bool? dialogResult = colorPicker.ShowDialog();
            if (dialogResult != null && (bool)dialogResult == true)
            {
                strokeBrushColor = new SolidColorBrush(colorPicker.SelectedColor);
                rectStrokeColor.Fill = strokeBrushColor;
            }
        }

        private void canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (!canvas1.IsMouseCaptured)
            {
                startPoint = new Point();
                startPoint = e.GetPosition(canvas1);
                canvas1.CaptureMouse();
                if (rbSelect.IsChecked == true)
                {
                    if (canvas1 == e.Source)
                    {
                        isSelected = false;
                        return;
                    }
                    else
                    {
                        selectedElement = (UIElement)e.Source;
                        isSelected = true;
                    }
                }

                if (rbMove.IsChecked == true && isSelected == true)
                {
                    StartMoving(selectedElement);
                }

                if (rbDrawPolygon.IsChecked == true || rbDrawPolyLine.IsChecked == true)
                {
                    PolyLineShapes.AddPointCircle(PolyLineShapes.AddPolyPoints(startPoint),pointCircles,canvas1);
                    polyLinePoints.Add(startPoint);
                }
            }
        }

        private void canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = new Point();
            endPoint = e.GetPosition(canvas1);

            //delete last temporary shape
            if (drawedUIE.Count > 0 && isMoving == false && isSelected == false)
            {
                canvas1.Children.Remove(drawedUIE[0]);
                drawedUIE.RemoveAt(0);
            }

            if (rbDrawLine.IsChecked == true)
            {
                LineShapes lineShape = new LineShapes();
                lineShape = (LineShapes)lineShape.CreateMyShapesObject(startPoint, endPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(lineShape);
                lineShape.DrawMyShape(canvas1, lineShape.CreateShape(lineShape));
            }
            else if (rbDrawSquare.IsChecked == true)
            {
                SquareShapes squareShape = new SquareShapes();
                squareShape = (SquareShapes)squareShape.CreateMyShapesObject(startPoint, endPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(squareShape);
                squareShape.DrawMyShape(canvas1, squareShape.CreateShape(squareShape));
            }
            else if (rbDrawRectangle.IsChecked == true)
            {
                RectangleShapes rectangleShape = new RectangleShapes();
                rectangleShape = (RectangleShapes)rectangleShape.CreateMyShapesObject(startPoint, endPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(rectangleShape);
                rectangleShape.DrawMyShape(canvas1, rectangleShape.CreateShape(rectangleShape));
            }
            else if (rbDrawCircle.IsChecked == true)
            {
                CircleShapes circleShape = new CircleShapes();
                circleShape = (CircleShapes)circleShape.CreateMyShapesObject(startPoint, endPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(circleShape);
                circleShape.DrawMyShape(canvas1, circleShape.CreateShape(circleShape));
            }
            else if (rbDrawEllipse.IsChecked == true)
            {
                EllipseShapes ellipseShape = new EllipseShapes();
                ellipseShape = (EllipseShapes)ellipseShape.CreateMyShapesObject(startPoint, endPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(ellipseShape);
                ellipseShape.DrawMyShape(canvas1, ellipseShape.CreateShape(ellipseShape));
            }
            else if (rbDrawArc.IsChecked == true)
            {
                if (ArcParmValidation() == false)
                {
                    Messages.InvalidParameters();
                }
                else
                {
                    ArcShapes arcShape = new ArcShapes();
                    arcShape = (ArcShapes)arcShape.CreateArcShapeObject(startPoint, endPoint, 
                                                                        strokeBrushColor, 
                                                                        Convert.ToDouble(arcWidth.Text), 
                                                                        Convert.ToDouble(arcHeight.Text), 
                                                                        Convert.ToDouble(arcAngle.Text), 
                                                                        arcSweepDirection.Text, 
                                                                        isLargeArc.IsChecked, 
                                                                        comboBoxSize);
                    myShapes.Add(arcShape);
                    arcShape.DrawMyShape(canvas1, arcShape.CreateArcShape(arcShape));

                    arcDesign.Visibility = System.Windows.Visibility.Hidden;
                    customDraw.Focus();
                    rbDrawArc.IsChecked = false;
                }
            }

            if (isMoving == true)
            {
                StopMoving();
            }

            canvas1.ReleaseMouseCapture();
        }

        private void canvas1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (canvas1.IsMouseCaptured)
            {
                if (isMoving == false && isSelected == false)
                {
                     currentPoint = e.GetPosition(canvas1);
                     if (drawedUIE.Count > 0)
                     {
                         canvas1.Children.Remove(drawedUIE[0]);
                         drawedUIE.RemoveAt(0);
                     }
                }
                    
                if (rbDrawLine.IsChecked == true)
                {
                    LineShapes lineShape = new LineShapes();
                    lineShape = (LineShapes)lineShape.CreateMyShapesObject(startPoint, currentPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                    drawedUIE.Add(lineShape.CreateShape(lineShape));
                    lineShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                }
                else if (rbDrawSquare.IsChecked == true)
                {
                    SquareShapes squareShape = new SquareShapes();
                    squareShape = (SquareShapes)squareShape.CreateMyShapesObject(startPoint, currentPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                    drawedUIE.Add(squareShape.CreateShape(squareShape));
                    squareShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                }
                else if (rbDrawRectangle.IsChecked == true)
                {
                    RectangleShapes rectangleShape = new RectangleShapes();
                    rectangleShape = (RectangleShapes)rectangleShape.CreateMyShapesObject(startPoint, currentPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                    drawedUIE.Add(rectangleShape.CreateShape(rectangleShape));
                    rectangleShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                }
                else if (rbDrawCircle.IsChecked == true)
                {
                    CircleShapes circleShape = new CircleShapes();
                    circleShape = (CircleShapes)circleShape.CreateMyShapesObject(startPoint, currentPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                    drawedUIE.Add(circleShape.CreateShape(circleShape));
                    circleShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                }
                else if (rbDrawEllipse.IsChecked == true)
                {
                    EllipseShapes ellipseShape = new EllipseShapes();
                    ellipseShape = (EllipseShapes)ellipseShape.CreateMyShapesObject(startPoint, currentPoint, strokeBrushColor, fillBrushColor, comboBoxSize);
                    drawedUIE.Add(ellipseShape.CreateShape(ellipseShape));
                    ellipseShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                }
                else if (rbDrawArc.IsChecked == true)
                {
                    if (ArcParmValidation() == true)
                    {
                        ArcShapes arcShape = new ArcShapes();
                        arcShape = (ArcShapes)arcShape.CreateArcShapeObject(startPoint, currentPoint,
                                                                            strokeBrushColor,
                                                                            Convert.ToDouble(arcWidth.Text),
                                                                            Convert.ToDouble(arcHeight.Text),
                                                                            Convert.ToDouble(arcAngle.Text),
                                                                            arcSweepDirection.Text,
                                                                            isLargeArc.IsChecked,
                                                                            comboBoxSize);
                        drawedUIE.Add(arcShape.CreateArcShape(arcShape));
                        arcShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                    }
                }

                if (isMoving == true)
                {
                    Moving(myShapes[index]);
                }
            }
        }

        bool ArcParmValidation()
        {
            bool isValid = false;
            double number;
            string arcHeightStr = arcHeight.Text;
            string arcWidthStr = arcWidth.Text;
            string arcAngleStr = arcAngle.Text;

            if (!double.TryParse(arcHeightStr, out number))
            {
                isValid = false;
                arcHeight.SelectAll();
            }
            else if (!double.TryParse(arcWidthStr, out number))
            {
                isValid = false;
                arcWidth.SelectAll();
            }
            else if (!double.TryParse(arcAngleStr, out number))
            {
                isValid = false;
                arcAngle.SelectAll();
            }
            else
            {
                isValid = true;
            }
            return isValid;
        }

        private void canvas1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            endPoint = e.GetPosition(canvas1);
            if (rbDrawPolyLine.IsChecked == true)
            {
                polyLinePoints.Add(endPoint);

                PolyLineShapes polyLineShape = new PolyLineShapes();
                polyLineShape = (PolyLineShapes)polyLineShape.CreatePolyLineObject(polyLinePoints,strokeBrushColor, comboBoxSize);
                myShapes.Add(polyLineShape);
                polyLineShape.DrawMyShape(canvas1, polyLineShape.CreatePolyLineShape(polyLineShape));
                polyLinePoints.RemoveRange(0, polyLinePoints.Count);
            }
            else if (rbDrawPolygon.IsChecked == true)
            {
                polyLinePoints.Add(endPoint);

                PolygonShapes polygonShape = new PolygonShapes();
                polygonShape = (PolygonShapes)polygonShape.CreatePolygonObject(polyLinePoints, strokeBrushColor, fillBrushColor, comboBoxSize);
                myShapes.Add(polygonShape);
                polygonShape.DrawMyShape(canvas1, polygonShape.CreatePolygonShape(polygonShape));
                polyLinePoints.RemoveRange(0, polyLinePoints.Count);
            }
            //delete circles which marked the points
            for (int i = 0; i < pointCircles.Count; i++)
            {
                canvas1.Children.Remove(pointCircles[i]);
                pointCircles[i] = null;
            }
            polyLinePoints.RemoveRange(0, polyLinePoints.Count);
        }

        private void drawShapeGroupChecked(object sender, RoutedEventArgs e)
        {
            if (rbDrawPolyLine.IsChecked == true || rbDrawPolygon.IsChecked == true)
            {
                Messages.PolyLineGonMessage();
            }
            if (rbDrawArc.IsChecked == true)
            {
                arcDesign.Visibility = System.Windows.Visibility.Visible;
                arcDesign.Focus();
            }

            rbSelect.IsChecked = false;
            isSelected = false;
            rbMove.IsChecked = false;
            isMoving = false;

        }

        private void transformGroup_Checked(object sender, RoutedEventArgs e)
        {
            rbDrawArc.IsChecked = false;
            rbDrawCircle.IsChecked = false;
            rbDrawEllipse.IsChecked = false;
            rbDrawLine.IsChecked = false;
            rbDrawPolygon.IsChecked = false;
            rbDrawPolyLine.IsChecked = false;
            rbDrawRectangle.IsChecked = false;
            rbDrawSquare.IsChecked = false;
        }

        private void StartMoving(UIElement selectedElement)
        {
            if (selectedElement == null)
            {
                return;
            }
            else
            {
                isMoving = true;
                currentPoint = Mouse.GetPosition(canvas1);
                drawedUIE.Add(selectedElement);
                index = MyShapes.FindSelectedObjectIndex(myShapes, selectedElement, index);
            }
        }

        private void Moving(MyShapes selectedMyShape)
        {

            Point newSP = new Point();
            Point newEP = new Point();
            Point newCP = Mouse.GetPosition(canvas1);
            double subtractionX = newCP.X - currentPoint.X;
            double subtractionY = newCP.Y - currentPoint.Y;

            newSP = new Point(selectedMyShape.StartPoint.X + subtractionX, selectedMyShape.StartPoint.Y + subtractionY);
            newEP = new Point(selectedMyShape.EndPoint.X + subtractionX, selectedMyShape.EndPoint.Y + subtractionY);
            currentPoint = newCP;
            selectedMyShape.StartPoint = newSP;
            selectedMyShape.EndPoint = newEP;

            canvas1.Children.Remove(drawedUIE[0]);
            drawedUIE.RemoveAt(0);


            if (selectedMyShape.ToString() == "DrawShape.PolygonShapes")
            {
                PolygonShapes polygonShape = (PolygonShapes)selectedMyShape;
                polygonShape = PolygonShapes.MovePolygon(subtractionX, subtractionY, polygonShape);
                drawedUIE.Add(polygonShape.CreatePolygonShape(polygonShape));
                polygonShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count-1]);
                selectedMyShape = (MyShapes)polygonShape;
            }
            else if (selectedMyShape.ToString() == "DrawShape.PolyLineShapes")
            {
                PolyLineShapes polylineShape = (PolyLineShapes)selectedMyShape;
                polylineShape = PolyLineShapes.MovePolyLine(subtractionX, subtractionY, polylineShape);
                drawedUIE.Add(polylineShape.CreatePolyLineShape(polylineShape));
                polylineShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                selectedMyShape = (MyShapes)polylineShape;
            }
            else if (selectedMyShape.ToString() == "DrawShape.ArcShapes")
            {
                ArcShapes arcShape = (ArcShapes)selectedMyShape;
                drawedUIE.Add(arcShape.CreateArcShape(arcShape));
                arcShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
                selectedMyShape = (MyShapes)arcShape;
            }

            else
            {
                drawedUIE.Add(selectedMyShape.CreateShape(selectedMyShape));
                selectedMyShape.DrawMyShape(canvas1, drawedUIE[drawedUIE.Count - 1]);
            }
        }

        private void StopMoving()
        {
            if (drawedUIE.Count > 0)
            {
                drawedUIE.RemoveAt(0);
            }
            currentPoint = new Point();
            isMoving = false;
            isSelected = false;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == true)
            {
                MessageBoxResult result = Messages.DeleteWarning();
                if (result == MessageBoxResult.Yes)
                {
                    canvas1.Children.Remove(selectedElement);
                    myShapes.RemoveAt(MyShapes.FindSelectedObjectIndex(myShapes, selectedElement, index));
                    isSelected = false;
                }
                else
                {
                    return;
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Messages.SaveWarning();
            if (result == MessageBoxResult.Yes)
            {
                Save();
            }

            
            canvas1.Children.Clear();
            myShapes.RemoveRange(0, myShapes.Count);
            sockTempArray[0] = null;
        }

        private void Save()
        {
            using (SaveFileDialog saveDlg = new SaveFileDialog())
            {
                // Configure the look and feel of the save dialog box.
                saveDlg.InitialDirectory = ".";
                saveDlg.Filter = "Shape files (*.dsf)|*.dsf";
                saveDlg.RestoreDirectory = true;
                saveDlg.FileName = "MyShapes";
                // If they click the OK button, open the new
                // file and serialize the List<T>.
                if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.Stream myStream = saveDlg.OpenFile();
                    if ((myStream != null))
                    {
                        // Save the shapes!
                        BinaryFormatter myBinaryFormat = new BinaryFormatter();
                        myBinaryFormat.Serialize(myStream, myShapes);
                        myBinaryFormat.Serialize(myStream, sockTempArray);
                        myStream.Close();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = Messages.SaveWarning();
            if (result == MessageBoxResult.Yes)
            {
                Save();
            }


            canvas1.Children.Clear();
            myShapes.RemoveRange(0, myShapes.Count);
            sockTempArray[0] = null;

            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.InitialDirectory = ".";
                openDlg.Filter = "Shape files (*.dsf)|*.dsf";
                openDlg.RestoreDirectory = true;
                openDlg.FileName = "MyShapes";
                if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.Stream myStream = openDlg.OpenFile();
                    if ((myStream != null))
                    {
                        // Get the shapes!
                        BinaryFormatter myBinaryFormat = new BinaryFormatter();
                        myShapes = (List<MyShapes>)myBinaryFormat.Deserialize(myStream);
                        sockTempArray = (SockTemp[])myBinaryFormat.Deserialize(myStream);
                        myStream.Close();
                        foreach (var myShape in myShapes)
                        {
                            if (myShape.ToString() == "DrawShape.LineShapes")
                            {
                                myShape.DrawMyShape(canvas1, myShape.CreateShape(myShape));
                            }
                            
                            else if (myShape.ToString() == "DrawShape.SquareShapes")
                            {
                                myShape.DrawMyShape(canvas1, myShape.CreateShape(myShape));
                            }
                            else if (myShape.ToString() == "DrawShape.RectangleShapes")
                            {
                                myShape.DrawMyShape(canvas1, myShape.CreateShape(myShape));
                            }
                            else if (myShape.ToString() == "DrawShape.CircleShapes")
                            {
                                myShape.DrawMyShape(canvas1, myShape.CreateShape(myShape));
                            }
                            else if (myShape.ToString() == "DrawShape.EllipseShapes")
                            {
                                myShape.DrawMyShape(canvas1, myShape.CreateShape(myShape));
                            }
                            else if (myShape.ToString() == "DrawShape.PolygonShapes")
                            {
                                PolygonShapes polygonShape = new PolygonShapes();
                                polygonShape = (PolygonShapes)myShape;
                                polygonShape.DrawMyShape(canvas1, polygonShape.CreatePolygonShape(polygonShape));
                            }
                            else if (myShape.ToString() == "DrawShape.PolyLineShapes")
                            {
                                PolyLineShapes polyLineShape = new PolyLineShapes();
                                polyLineShape = (PolyLineShapes)myShape;
                                polyLineShape.DrawMyShape(canvas1, polyLineShape.CreatePolyLineShape(polyLineShape));
                            }
                            else if (myShape.ToString() == "DrawShape.ArcShapes")
                            {
                                ArcShapes arcShape = new ArcShapes();
                                arcShape = (ArcShapes)myShape;
                                arcShape.DrawMyShape(canvas1, arcShape.CreateArcShape(arcShape));
                            }
                        }
                        foreach (var sockTemp in sockTempArray)
                        {
                            if (sockTemp != null)
                            {
                                sockTemp.DrawSockTemp(canvas1);
                                if (chShowSizes.IsChecked == true)
                                {
                                    sockTemp.DrawSizes(canvas1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Zoom()
        {
            if (tbZoom != null)
            {
                tbZoom.Text = (sliderZoom.Value * 100).ToString() + "%";
                ScaleTransform st = new ScaleTransform();
                st.ScaleX = sliderZoom.Value;
                st.ScaleY = sliderZoom.Value;
                canvas1.LayoutTransform = st;
            }
            
        }

        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Zoom();
            if (chShowGrid != null)
            {
                if (chShowGrid.IsChecked == true)
                {
                    canvas1.Background = DrawGrid();

                }
            }
        }

        private void btnSockTemp_Click(object sender, RoutedEventArgs e)
        {
            if (sockTempArray[0] == null )
            {
                //Window for entering sock's sizes is showing
                WindowSockTemp window = new WindowSockTemp();
                window.ShowDialog();
                if (WindowSockTemp.isSizesOK == true)
                {
                    SockTemp sockTemp = new SockTemp(WindowSockTemp.sockSizes);
                    sockTempArray[0] = sockTemp;
                    sockTempArray[0].DrawSockTemp(canvas1);
                }
            }
        }

        private void chShowSizes_Checked(object sender, RoutedEventArgs e)
        {
            if (sockTempArray[0] != null)
            {
                sockTempArray[0].DrawSizes(canvas1);
            }
        }

        private void chShowSizes_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sockTempArray[0] != null)
            {
                sockTempArray[0].RemoveDrawedSizes(canvas1);
            }
        }

        private void btnDeleteTemp_Click(object sender, RoutedEventArgs e)
        {
            if (sockTempArray[0] != null)
            {
                sockTempArray[0].RemoveDrawedSizes(canvas1);
                sockTempArray[0].RemoveDrawedTemp(canvas1);
                sockTempArray[0] = null;
                for (int i = 0; i < WindowSockTemp.sockSizes.Length; i++)
                {
                    WindowSockTemp.sockSizes[i] = 0;
                }
                WindowSockTemp.isSizesOK = false;
            }
        }

        private void chShowGrid_Checked(object sender, RoutedEventArgs e)
        {
            // Use the DrawingBrush to paint the button's background.
            canvas1.Background = DrawGrid();

        }

        private DrawingBrush DrawGrid()
        {
            double cm = 96/2.54;

            if (sliderZoom.Value >=2 && sliderZoom.Value < 3)
            {
                cm = cm / 5;
            }
            if (sliderZoom.Value >= 3)
            {
                cm = cm / 10;
            }
            // Create a DrawingBrush.
            DrawingBrush myDrawingBrush = new DrawingBrush();

            // Create a drawing.
            GeometryDrawing myGeometryDrawing = new GeometryDrawing();
            myGeometryDrawing.Brush = Brushes.White;
            myGeometryDrawing.Pen = new Pen(Brushes.Green, 0.3);

            GeometryGroup lines = new GeometryGroup();

            myDrawingBrush.Viewport = new Rect(new Point(0, 0), new Point(5 * cm, 5 * cm));
            myDrawingBrush.ViewportUnits = BrushMappingMode.Absolute;
            myDrawingBrush.TileMode = TileMode.Tile;

            Rect rect = new Rect();
            rect.Width = 5*cm;
            rect.Height = 5*cm;
            rect.X = 0;
            rect.Y = 0;

            lines.Children.Add(new RectangleGeometry(rect));
            lines.Children.Add(new LineGeometry(new Point(0, 0), new Point(5 * cm, 0)));
            lines.Children.Add(new LineGeometry(new Point(0, cm), new Point(5 * cm, cm)));
            lines.Children.Add(new LineGeometry(new Point(0, 2 * cm), new Point(5 * cm, 2 * cm)));
            lines.Children.Add(new LineGeometry(new Point(0, 3 * cm), new Point(5 * cm, 3 * cm)));
            lines.Children.Add(new LineGeometry(new Point(0, 4 * cm), new Point(5 * cm, 4 * cm)));

            lines.Children.Add(new LineGeometry(new Point(0, 0), new Point(0, 5 * cm)));
            lines.Children.Add(new LineGeometry(new Point(cm, 0), new Point(cm, 5 * cm)));
            lines.Children.Add(new LineGeometry(new Point(2 * cm, 0), new Point(2 * cm, 5 * cm)));
            lines.Children.Add(new LineGeometry(new Point(3 * cm, 0), new Point(3 * cm, 5 * cm)));
            lines.Children.Add(new LineGeometry(new Point(4 * cm, 0), new Point(4 * cm, 5 * cm)));

            myGeometryDrawing.Geometry = lines;

            myDrawingBrush.Drawing = myGeometryDrawing;

            return myDrawingBrush;

        }

        private void chShowGrid_Unchecked(object sender, RoutedEventArgs e)
        {
            canvas1.Background = Brushes.White;
        }
    }
}


