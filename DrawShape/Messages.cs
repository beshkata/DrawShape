using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawShape
{
    class Messages
    {
        public static void PolyLineGonMessage()
        {
            string msg =
            "Click with left mouse button to create points. Click with right mouse button for final point";
            string title = "Final Point";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);

        }

        public static void PolygonMultiSelectMessage()
        {
            string msg =
            "Polygon shape can't selected in multiselect mode";
            string title = "Polygon Selection";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);

        }

        public static void InvalidParameters()
        {
            string msg ="One or more parameters are invalid";
            string title = "Invalid parameters";
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);

        }

        public static MessageBoxResult DeleteWarning()
        {
            string msg = "Do you really want to delete this shape?";
            string title = "Delete Shape?";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);

            return result;
        }

        public static MessageBoxResult SaveWarning()
        {
            string msg = "Would you like to save your work?";
            string title = "Save project";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result =
            MessageBox.Show(msg, title, buttons, icon);

            return result;
        }
    }
}
