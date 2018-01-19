using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryTransfer.View
{
    public partial class FW_PDE_Deliveries : FloatWindows.PurchaseDeliverySelectWindow
    {
        public FW_PDE_Deliveries(List<PurchaseDelivery> Documents, Provider provider):base(Documents, provider)
        {
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Transfer)a.MainFrame.Content;
        }
    }
}
