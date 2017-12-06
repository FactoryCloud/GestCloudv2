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

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.View
{
    /// <summary>
    /// Interaction logic for TS_PUR_Menu.xaml
    /// </summary>
    public partial class TS_POR_Menu : Page
    {
        public TS_POR_Menu()
        {
            InitializeComponent();
            if (GetController().purchaseOrder != null)
            {
                BT_PurchaseAdjustLoad.IsEnabled = true;
                BT_PurchaseAdjustLoadEditable.IsEnabled = true;
            }
        }

        private void EV_MD_New_PurchaseOrder(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_OrderPurchaseNew();
        }

        private void EV_MD_PurchaseOrderLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StockAdjustLoad();
        }

        private void EV_MD_PurchaseOrderLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_PurchaseOrderLoadEditable();
        }

        private Controller.CT_PurchaseOrderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseOrderMenu)a.MainFrame.Content;
        }
    }
}
