using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryTransfer.View
{
    class TS_PDE_Transfer : GestCloudv2.Documents.DCM_Transfers.View.TS_DCM_Transfers
    {
        virtual public Controller.CT_PDE_Transfer GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Transfer)a.MainFrame.Content;
        }
    }
}
