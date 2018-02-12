using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferMenu.View
{
    class TS_STT_Menu : Documents.DCM_Menu.View.TS_DCM_Menu
    {
        public TS_STT_Menu():base()
        {
            BT_DeliveryTransfer.Visibility = Visibility.Hidden;
            BT_InvoiceTransfer.Visibility = Visibility.Hidden;

            LB_New.Content = "Traspaso entre Almacenes";
            LB_Load.Content = "Traspaso entre Almacenes";
            LB_LoadEditable.Content = "Traspaso entre Almacenes";
        }

        override public Documents.DCM_Menu.Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_StoreTransferMenu)a.MainFrame.Content;
        }
    }
}
