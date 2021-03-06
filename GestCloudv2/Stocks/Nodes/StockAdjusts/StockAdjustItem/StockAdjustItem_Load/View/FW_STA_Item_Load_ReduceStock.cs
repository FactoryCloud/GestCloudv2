﻿using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_Load.View
{
    public partial class FW_STA_Item_Load_ReduceStock : FloatWindows.StoredStockSelectWindow
    {
        public FW_STA_Item_Load_ReduceStock(int option, List<Movement> movements):base(option, movements)
        {
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (StockAdjustItem_Load.Controller.CT_STA_Item_Load)a.MainFrame.Content;
        }
    }
}
