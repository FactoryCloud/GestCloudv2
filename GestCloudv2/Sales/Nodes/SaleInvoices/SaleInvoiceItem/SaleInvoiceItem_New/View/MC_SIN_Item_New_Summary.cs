﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceItem.SaleInvoiceItem_New.View
{
    class MC_SIN_Item_New_Summary : Documents.DCM_Items.DCM_Item_New.View.MC_DCM_Item_New_Summary
    {
        override public Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SIN_Item_New)a.MainFrame.Content;
        }
    }
}
