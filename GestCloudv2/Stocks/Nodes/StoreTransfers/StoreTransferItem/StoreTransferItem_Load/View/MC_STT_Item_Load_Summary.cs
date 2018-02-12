using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GestCloudv2.Stocks.Nodes.StoreTransfers.StoreTransferItem.StoreTransferItem_Load.View
{
    class MC_STT_Item_Load_Summary : Documents.DCM_Items.DCM_Item_Load.View.MC_DCM_Item_Load_Summary
    {
        override public Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STT_Item_Load)a.MainFrame.Content;
        }
    }
}
