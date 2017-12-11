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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.View
{
    /// <summary>
    /// Interaction logic for TS_PUR_Menu.xaml
    /// </summary>
    public partial class TS_PDE_Menu : Page
    {
        public TS_PDE_Menu()
        {
            InitializeComponent();
            if (GetController().purchaseDelivery != null)
            {
                BT_PurchaseAdjustLoad.IsEnabled = true;
                BT_PurchaseAdjustLoadEditable.IsEnabled = true;
            }
        }

        private void EV_MD_New_PurchaseDelivery(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_DeliveryPurchaseNew();
        }

        private void EV_MD_PurchaseDeliveryLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_PurchaseDeliveryLoad();
        }

        private void EV_MD_PurchaseDeliveryLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_PurchaseDeliveryLoadEditable();
        }

        private Controller.CT_PurchaseDeliveryMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseDeliveryMenu)a.MainFrame.Content;
        }
    }
}
