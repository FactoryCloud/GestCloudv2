using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderTransfer.SOR_Transfer_Invoice.View
{
    class MC_SOR_Transfer_Invoice : GestCloudv2.Documents.DCM_Transfers.View.MC_DCM_Transfers
    {
        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SOR_Transfer_Invoice)a.MainFrame.Content;
        }
    }
}
