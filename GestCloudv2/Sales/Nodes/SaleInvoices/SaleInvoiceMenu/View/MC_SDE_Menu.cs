using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceMenu.View
{
    public class MC_SDE_Menu : Documents.DCM_Menu.View.MC_DCM_Menu
    {
        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SaleInvoiceMenu)a.MainFrame.Content;
        }
    }
}
