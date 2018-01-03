using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_Load.View
{
    class TS_POR_Item_Load_PurchaseOrder : Documents.DCM_Items.DCM_Item_Load.View.TS_DCM_Item_Load_Main
    {
        public TS_POR_Item_Load_PurchaseOrder(int num):base(num)
        {

        }

        override public Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Item_Load)a.MainFrame.Content;
        }
    }
}
