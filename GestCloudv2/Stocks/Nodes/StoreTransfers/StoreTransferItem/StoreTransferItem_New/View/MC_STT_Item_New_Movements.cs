﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_New.View
{
    class MC_STT_Item_New_Movements : Documents.DCM_Items.DCM_Item_New.View.MC_DCM_Item_New_Movements
    {
        override public Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STT_Item_New)a.MainFrame.Content;
        }
    }
}
