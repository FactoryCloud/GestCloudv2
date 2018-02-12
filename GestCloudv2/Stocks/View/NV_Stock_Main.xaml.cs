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

namespace GestCloudv2.Stocks.View
{
    /// <summary>
    /// Interaction logic for NV_Stocks_Main.xaml
    /// </summary>
    public partial class NV_Stocks_Main : Page
    {
        public NV_Stocks_Main()
        {
            InitializeComponent();
        }

        private void EV_CT_StoreTransfers(object sender, RoutedEventArgs e)
        {
            GetController().CT_StoreTransfers();
        }

        private void EV_CT_StockAdjusts(object sender, RoutedEventArgs e)
        {
            GetController().CT_StockAdjusts();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Controller.CT_Stocks GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_Stocks)a.MainFrame.Content;
        } 
    }
}
