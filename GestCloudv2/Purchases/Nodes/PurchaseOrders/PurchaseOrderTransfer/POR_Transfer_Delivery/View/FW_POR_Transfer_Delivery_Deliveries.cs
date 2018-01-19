using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderTransfer.POR_Transfer_Delivery.View
{
    public partial class FW_POR_Transfer_Delivery_Deliveries : FloatWindows.PurchaseDeliverySelectWindow
    {
        public FW_POR_Transfer_Delivery_Deliveries(Provider provider):base(provider)
        {
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Transfer_Delivery)a.MainFrame.Content;
        }
    }
}
