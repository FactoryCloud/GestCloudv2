﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceItem.PurchaseInvoiceItem_New.View
{
    class TS_PIN_Item_New_PurchaseInvoice : Documents.DCM_Items.DCM_Item_New.View.TS_DCM_Item_New_Main
    {
        override public Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PIN_Item_New)a.MainFrame.Content;
        }
    }
}
