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
using GestCloudv2;
using FrameworkDB.V1;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderItem.SaleOrderItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_POR_Item_New_PurchaseOrder.xaml
    /// </summary>
    public partial class NV_SOR_Item_New_SaleOrder : Page
    {
        public NV_SOR_Item_New_SaleOrder()
        {
            InitializeComponent();
        }

        private void EV_MD_Headboard(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1);
        }

        private void EV_MD_Movements(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_SOR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SOR_Item_New)a.MainFrame.Content;
        }
    }
}
