using Microsoft.Win32;
using System;
using System.Windows;


namespace Lb5_9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculator Calculator1;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Calculator1 = new Calculator();

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

                strX = sX[0] + "; " + sX[1] + "; " + sX[2];
                strY = sY[0] + "; " + sY[1] + "; " + sY[2];

                Calculator1.Values.Add(v);
                LBInputSets.Items.Add("X: " + strX + "\n" + "Y: " + strY);

                TBInputX.Text = "";
                TBInputY.Text = "";
            }
            catch { MessageBox.Show("Некорректные данные"); }

        }

        private void BtnLBInputSetsClear_Click(object sender, RoutedEventArgs e)
        {
            LBInputSets.Items.Clear();
            Calculator1.Values.Clear();
        }


        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculator1.Calculate();
        }



        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.CurrentDirectory;//стартовая директория - директория текущего приложения
            ofd.Title = "выберите 1 файл";
            ofd.Filter = "Файл |*cnt64; *dat;";
            if (ofd.ShowDialog() == true)
                Calculator1.Read(ofd.FileName);
        }
    }
}
