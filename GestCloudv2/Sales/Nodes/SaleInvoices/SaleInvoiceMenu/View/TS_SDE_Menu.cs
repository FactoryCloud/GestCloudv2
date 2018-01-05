using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceMenu.View
{
    class TS_SDE_Menu : Documents.DCM_Menu.View.TS_DCM_Menu
    {
        public TS_SDE_Menu() : base()
        {
            LB_New.Content = "Factura";
            LB_Load.Content = "Factura";
            LB_LoadEditable.Content = "Factura";
        }

        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SaleInvoiceMenu)a.MainFrame.Content;
        }
    }
}
