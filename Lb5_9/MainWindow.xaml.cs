using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Lb5_9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculate Calculate1;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Calculate1 = new Calculate();

        }

        private void BtnInput_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strX = TBInputX.Text.Replace(" ", "");
                string strY = TBInputY.Text.Replace(" ", "");

                string[] sX = strX.Split(';');
                string[] sY = strY.Split(';');

                ValueStruct v = new ValueStruct
                {
                    Xo = Convert.ToInt32(sX[0]),
                    Xn = Convert.ToInt32(sX[1]),
                    XStep = Convert.ToInt32(sX[2]),
                    Yo = Convert.ToInt32(sY[0]),
                    Yn = Convert.ToInt32(sY[1]),
                    YCount = Convert.ToInt32(sY[2])
                };

                strX = sX[0] + "; " + sX[1] + "; " + sY[2];
                strY = sY[0] + "; " + sY[1] + "; " + sY[2];

                Calculate1.Values.Add(v);

                string newSet = "X: " + strX + "\n" + "Y: " + strY;
                LBInputSets.Items.Add(newSet);
                CBSelectSet.Items.Add(newSet);

                TBInputX.Text = "";
                TBInputY.Text = "";
            }
            catch (Exception) { MessageBox.Show("Некорректные данные"); }

        }



    }
}
