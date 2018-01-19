using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderTransfer.POR_Transfer_Delivery.View
{
    class MC_POR_Transfer_Delivery : GestCloudv2.Documents.DCM_Transfers.View.MC_DCM_Transfers
    {
        public MC_POR_Transfer_Delivery()
        {
            LB_Document.Content = $"Albarán";

            BT_Invoice.Visibility = Visibility.Hidden;
            BT_Delivery.Visibility = Visibility.Visible;
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Transfer_Delivery)a.MainFrame.Content;
        }
    }
}
