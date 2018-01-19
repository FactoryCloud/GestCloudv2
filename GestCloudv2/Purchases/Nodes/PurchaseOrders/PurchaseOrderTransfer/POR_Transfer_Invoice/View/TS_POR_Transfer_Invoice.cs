using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderTransfer.POR_Transfer_Invoice.View
{
    class TS_POR_Transfer_Invoice : GestCloudv2.Documents.DCM_Transfers.View.TS_DCM_Transfers
    {
        public TS_POR_Transfer_Invoice()
        {
            BT_AddDocument1.Visibility = Visibility.Hidden;
            BT_AddDocument2.Visibility = Visibility.Visible;
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Transfer_Invoice)a.MainFrame.Content;
        }
    }
}
