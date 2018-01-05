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

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_New.View
{
    public partial class FW_SOR_Item_New_SelectClient : FloatWindows.ClientSelectWindow
    {
        public FW_SOR_Item_New_SelectClient(int option) : base(option)
        {
            
        }

        public FW_SOR_Item_New_SelectClient(int option, int cli) : base(option, cli)
        {

        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (SaleOrderItem_New.Controller.CT_SOR_Item_New)a.MainFrame.Content;
        }
    }
}
