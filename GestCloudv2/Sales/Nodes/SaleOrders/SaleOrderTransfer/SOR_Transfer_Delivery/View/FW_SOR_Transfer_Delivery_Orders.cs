using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderTransfer.SOR_Transfer_Delivery.View
{
    public partial class FW_SOR_Transfer_Delivery_Orders : FloatWindows.SaleOrderSelectWindow
    {
        public FW_SOR_Transfer_Delivery_Orders(List<SaleOrder> Documents, Client client):base(Documents, client)
        {
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SOR_Transfer_Delivery)a.MainFrame.Content;
        }
    }
}
