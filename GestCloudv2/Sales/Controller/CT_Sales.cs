using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Controller
{
    public partial class CT_Sales : Main.Controller.CT_Common
    {
        public CT_Sales()
        {
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public override void EV_UpdateShortcutDocuments(int option)
        {
            base.EV_UpdateShortcutDocuments(option);
            SC_Page = new View.SC_Sale_Main();
            RightSide.Content = SC_Page;
        }

        public void CT_SalesAdjusts()
        {
            Information["controller"] = 3;
            ChangeController();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public override void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_Sale_Main();
                    TS_Page = null;
                    MC_Page = null;
                    SC_Page = new View.SC_Sale_Main();
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

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Nodes.SaleOrders.SaleOrderMenu.Controller.CT_SaleOrderMenu();
                    break;
            }
        }
    }
}

