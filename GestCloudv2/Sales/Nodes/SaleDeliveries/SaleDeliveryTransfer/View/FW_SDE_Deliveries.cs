using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryTransfer.View
{
    public partial class FW_SDE_Deliveries : FloatWindows.SaleDeliverySelectWindow
    {
        public FW_SDE_Deliveries(List<SaleDelivery> Documents, Client client):base(Documents, client)
        {
        }

        override public Documents.DCM_Transfers.Controller.CT_DCM_Transfers GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SDE_Transfer)a.MainFrame.Content;
        }
    }
}
