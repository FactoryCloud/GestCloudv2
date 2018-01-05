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

namespace GestCloudv2.Sales.View
{
    /// <summary>
    /// Interaction logic for NV_Stocks_Main.xaml
    /// </summary>
    public partial class NV_Sale_Main : Page
    {
        public NV_Sale_Main()
        {
            InitializeComponent();
        }

        private void EV_Orders(object sender, RoutedEventArgs e)
        {
            GetController().CT_Orders();
        }

        private void EV_Deliveries(object sender, RoutedEventArgs e)
        {
            GetController().CT_Deliveries();
        }

        private void EV_Invoices(object sender, RoutedEventArgs e)
        {
            GetController().CT_Invoices();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Controller.CT_Sales GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Sales)a.MainFrame.Content;
        } 
    }
}
