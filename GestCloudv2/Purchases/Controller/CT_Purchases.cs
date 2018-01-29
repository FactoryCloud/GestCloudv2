using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Controller
{
    public partial class CT_Purchases : Main.Controller.CT_Common
    {
        public CT_Purchases()
        {
            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public override void SetSC()
        {
            SC_Page = new View.SC_Purchase_Main();
        }

        public void CT_PurchaseOrders()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_PurchaseDeliveries()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void CT_PurchaseInvoices()
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
                    NV_Page = new View.NV_Purchase_Main();
                    TS_Page = null;
                    MC_Page = null;
                    SetSC();
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
            switch (Information["controller"])
            {
                case 0:
                    a.MainFrame.Content = new Main.Controller.CT_Main();
                    break;

                case 1:
                    a.MainFrame.Content = new Nodes.PurchaseOrders.PurchaseOrderMenu.Controller.CT_PurchaseOrderMenu();
                    break;

                case 2:
                    a.MainFrame.Content = new Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.Controller.CT_PurchaseDeliveryMenu();
                    break;

                case 3:
                    a.MainFrame.Content = new Nodes.PurchaseInvoices.PurchaseInvoiceMenu.Controller.CT_PurchaseInvoiceMenu();
                    break;
            }
        }
    }
}

