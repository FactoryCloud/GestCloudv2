using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderTransfer.POR_Transfer_Invoice.View
{
    public partial class FW_POR_Transfer_Invoice_Invoices : FloatWindows.PurchaseInvoiceSelectWindow
    {
        public FW_POR_Transfer_Invoice_Invoices(Provider provider):base(provider)
        {
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Transfer_Invoice)a.MainFrame.Content;
        }
    }
}
