using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceMenu.View
{
    class MC_PIN_Menu : Documents.DCM_Menu.View.MC_DCM_Menu
    {
        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseInvoiceMenu)a.MainFrame.Content;
        }
    }
}
