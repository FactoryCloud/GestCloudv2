﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceItem.PurchaseInvoiceItem_Load.View
{
    class MC_PIN_Item_Load_Movements : Documents.DCM_Items.DCM_Item_Load.View.MC_DCM_Item_Load_Movements
    {
        override public Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PIN_Item_Load)a.MainFrame.Content;
        }
    }
}
