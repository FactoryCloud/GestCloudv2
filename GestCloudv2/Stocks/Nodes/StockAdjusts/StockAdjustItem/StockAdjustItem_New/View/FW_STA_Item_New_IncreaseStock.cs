using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.View
{
    public partial class FW_STA_Item_New_IncreaseStock : FloatWindows.ProductSelectWindow
    {
        public FW_STA_Item_New_IncreaseStock(int option, List<Movement> movements) : base(option, movements)
        {
            
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (StockAdjustItem_New.Controller.CT_STA_Item_New)a.MainFrame.Content;
        }
    }
}
