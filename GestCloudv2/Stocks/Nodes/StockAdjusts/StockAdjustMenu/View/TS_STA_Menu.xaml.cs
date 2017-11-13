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

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustMenu.View
{
    /// <summary>
    /// Interaction logic for TS_STA_Menu.xaml
    /// </summary>
    public partial class TS_STA_Menu : Page
    {
        public TS_STA_Menu()
        {
            InitializeComponent();
        }

        private void EV_MD_StockAdjustNew(object sender, RoutedEventArgs e)
        {
            //GetController().StartAddStock();
        }

        private Main.Controller.CT_Main GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Main)a.MainFrame.Content;
        }
    }
}
