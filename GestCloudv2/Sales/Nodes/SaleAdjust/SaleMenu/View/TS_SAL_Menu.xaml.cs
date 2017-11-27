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

namespace GestCloudv2.Sales.Nodes.SaleAdjust.SaleMenu.View
{
    /// <summary>
    /// Interaction logic for TS_PUR_Menu.xaml
    /// </summary>
    public partial class TS_SAL_Menu : Page
    {
        public TS_SAL_Menu()
        {
            InitializeComponent();
            /*if (GetController().stockAdjust != null)
            {
                BT_StockAdjustLoad.IsEnabled = true;
                BT_StockAdjustLoadEditable.IsEnabled = true;
            }*/
        }

        /*private void EV_MD_StockAdjustNew(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StockAdjustNew();
        }

        private void EV_MD_StockAdjustLoad(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StockAdjustLoad();
        }

        private void EV_MD_StockAdjustLoadEditable(object sender, RoutedEventArgs e)
        {
            GetController().EV_CT_StockAdjustLoadEditable();
        }

        private Controller.CT_StockAdjustMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_StockAdjustMenu)a.MainFrame.Content;
        }*/
    }
}
