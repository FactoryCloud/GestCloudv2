using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryTransfer.View
{
    class TS_SDE_Transfer : GestCloudv2.Documents.DCM_Transfers.View.TS_DCM_Transfers
    {
        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SDE_Transfer)a.MainFrame.Content;
        }
    }
}
