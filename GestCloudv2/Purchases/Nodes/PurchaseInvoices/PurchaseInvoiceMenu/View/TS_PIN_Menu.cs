using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceMenu.View
{
    class TS_PIN_Menu : Documents.DCM_Menu.View.TS_DCM_Menu
    {
        public TS_PIN_Menu():base()
        {
            LB_New.Content = "Factura";
            LB_Load.Content = "Factura";
            LB_LoadEditable.Content = "Factura";
            BT_DeliveryTransfer.Visibility = Visibility.Hidden;
            BT_InvoiceTransfer.Visibility = Visibility.Hidden;
        }

        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseInvoiceMenu)a.MainFrame.Content;
        }
    }
}
