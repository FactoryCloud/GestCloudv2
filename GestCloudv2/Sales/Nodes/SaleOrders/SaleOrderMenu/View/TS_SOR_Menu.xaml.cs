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

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderMenu.View
{
    /// <summary>
    /// Interaction logic for TS_PUR_Menu.xaml
    /// </summary>
    public partial class TS_SOR_Menu : Page
    {
        public TS_SOR_Menu()
        {
            InitializeComponent();
            if (GetController().saleOrder != null)
            {
                BT_SaleOrderLoad.IsEnabled = true;
                BT_SaleOrderLoadEditable.IsEnabled = true;
            }
        }

        private void EV_MD_New_SaleOrder(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_OrderSaleNew();
        }


        private void EV_MD_SaleOrderLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_SaleOrderLoad();
        }

        private void EV_MD_SaleOrderLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_SaleOrderLoadEditable();
        }

        private Controller.CT_SaleOrderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SaleOrderMenu)a.MainFrame.Content;
        }
    }
}
