﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    class NV_PDE_Item_New_PurchaseDelivery : Documents.DCM_Items.DCM_Item_New.View.NV_DCM_Item_New_Main
    {
        override public Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}
