using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryMenu.View
{
    public class MC_SDE_Menu : Documents.DCM_Menu.View.MC_DCM_Menu
    {
        virtual public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SaleDeliveryMenu)a.MainFrame.Content;
        }
    }
}
