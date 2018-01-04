using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.View
{
    class MC_POR_Menu : Documents.DCM_Menu.View.MC_DCM_Menu
    {
        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseOrderMenu)a.MainFrame.Content;
        }
    }
}
