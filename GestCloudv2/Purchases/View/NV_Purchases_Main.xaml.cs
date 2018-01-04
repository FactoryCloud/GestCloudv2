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

namespace GestCloudv2.Purchases.View
{
    /// <summary>
    /// Interaction logic for NV_Stocks_Main.xaml
    /// </summary>
    public partial class NV_Purchase_Main : Page
    {
        public NV_Purchase_Main()
        {
            InitializeComponent();
        }

        private void EV_Orders(object sender, RoutedEventArgs e)
        {
            GetController().CT_PurchaseOrders();
        }

        private void EV_Deliveries(object sender, RoutedEventArgs e)
        {
            GetController().CT_PurchaseDeliveries();
        }

        private void EV_Invoices(object sender, RoutedEventArgs e)
        {
            GetController().CT_PurchaseInvoices();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Controller.CT_Purchases GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Purchases)a.MainFrame.Content;
        }
    }
}
