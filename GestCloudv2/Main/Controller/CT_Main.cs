using FrameworkView.V1;
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
using FrameworkDB.V1;

namespace GestCloudv2.Main.Controller
{
    /// <summary>
    /// Interaction logic for CT_Main.xaml
    /// </summary>
    public partial class CT_Main : Main.Controller.CT_Common
    {
        public CT_Main()
        {
            InitializeComponent();
        }

        public override void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void CT_MainBack()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public void CT_Files()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_Stocks()
        {
            Information["controller"] = 4;
            ChangeController();
        }

        public void CT_Purchases()
        {
            Information["controller"] = 5;
            ChangeController();
        }

        public void CT_Sales()
        {
            Information["controller"] = 6;
            ChangeController();
        }

        public override void UpdateComponents ()
        {
            switch(Information["mode"])
            {
                case 1:
                    NV_Page = new View.NV_Main();
                    MC_Page = null;
                    TS_Page = new View.TS_Main();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 4:
                    Main.View.MainWindow e = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    e.MainFrame.Content = new Stocks.Controller.CT_Stocks();
                    break;

                case 5:
                    Main.View.MainWindow f = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    f.MainFrame.Content = new Purchases.Controller.CT_Purchases();
                    break;

                case 6:
                    Main.View.MainWindow g = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    g.MainFrame.Content = new Sales.Controller.CT_Sales();
                    break;
            }
        }
    }
}
