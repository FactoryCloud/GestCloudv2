using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_POR_Item_New_PurchaseOrder.xaml
    /// </summary>
    public partial class TS_SOR_Item_New_SaleOrder : Page
    {
        public TS_SOR_Item_New_SaleOrder(int num)
        {
            InitializeComponent();

            if (num >= 1)
            {
                BT_StockAdjustSave.IsEnabled = true;
            }
        }

        private void EV_CompanySave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewStockAdjust();
        }

        private Controller.CT_SOR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SOR_Item_New)a.MainFrame.Content;
        }
    }
}
