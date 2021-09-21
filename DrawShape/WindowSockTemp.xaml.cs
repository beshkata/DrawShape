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
using System.Windows.Shapes;

namespace DrawShape
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowSockTemp : Window
    {
        public static double [] sockSizes = new double [5];
        bool isValid = true;
        public static bool isSizesOK = false;

        double sizeA;
        double sizeB;
        double sizeC;
        double sizeD;
        double sizeE;

        public WindowSockTemp()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            isValid = true;
            tblockWarning.Text = "";
            string sizeAstr = tbAsize.Text;
            string sizeBstr = tbBsize.Text;
            string sizeCstr = tbCsize.Text;
            string sizeDstr = tbDsize.Text;
            string sizeEstr = tbEsize.Text;

            if (!double.TryParse(sizeAstr, out sizeA))
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + "Incorrect input in A: text box.";
            }
            if (!double.TryParse(sizeBstr, out sizeB))
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + "Incorrect input in B: text box.";
            }
            if (!double.TryParse(sizeCstr, out sizeC))
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + "Incorrect input in C: text box.";
            }
            if (!double.TryParse(sizeDstr, out sizeD))
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + "Incorrect input in D: text box.";
            }
            if (!double.TryParse(sizeEstr, out sizeE))
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + "Incorrect input in E: text box.";
            }
            if (isValid)
            {
                if (sizeA < sizeD)
                {
                    isValid = false;
                    tblockWarning.Text = tblockWarning.Text + " Size A must be bigger than size D!";
                }
                if (sizeB < sizeC)
                {
                    isValid = false;
                    tblockWarning.Text = tblockWarning.Text + " Size B must be bigger than size C!";
                }
                if (sizeC < sizeE)
                {
                    isValid = false;
                    tblockWarning.Text = tblockWarning.Text + " Size C must be bigger than size E!";
                }
                if (sizeB < sizeE)
                {
                    isValid = false;
                    tblockWarning.Text = tblockWarning.Text + " Size B must be bigger than size E!";
                }
                if (sizeC < 2*(sizeC - sizeE))
                {
                    isValid = false;
                    double number = sizeC / 2;
                    tblockWarning.Text = tblockWarning.Text + " Size E must be bigger than 2*(size C - size E)! If size C = " + sizeC.ToString() + ", than E must be bigger than " + number.ToString() ;
                }
            }
            if ((sizeA == 0 || sizeB == 0 || sizeC == 0 || sizeD == 0 || sizeE == 0) && isValid == true)
            {
                isValid = false;
                tblockWarning.Text = tblockWarning.Text + " Size cannot be 0 ";
            }
            if (isValid)
            {
                //converting sizes from cm to px 
                sockSizes[0] = sizeA * (96 / 2.54);
                sockSizes[1] = sizeB * (96 / 2.54);
                sockSizes[2] = sizeC * (96 / 2.54);
                sockSizes[3] = sizeD * (96 / 2.54);
                sockSizes[4] = sizeE * (96 / 2.54);

                isSizesOK = true;

                //close sock sizes window
                this.Close();
            }
        }
    }
}
