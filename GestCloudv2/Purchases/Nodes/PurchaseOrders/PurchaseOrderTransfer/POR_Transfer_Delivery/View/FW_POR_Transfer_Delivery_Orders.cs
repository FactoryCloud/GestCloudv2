﻿using FrameworkDB.V1;
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
    public partial class FW_POR_Transfer_Delivery_Orders : FloatWindows.PurchaseOrderSelectWindow
    {
        public FW_POR_Transfer_Delivery_Orders(List<PurchaseOrder> Documents, Provider provider):base(Documents, provider)
        {
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Transfer_Delivery)a.MainFrame.Content;
        }
    }
}
