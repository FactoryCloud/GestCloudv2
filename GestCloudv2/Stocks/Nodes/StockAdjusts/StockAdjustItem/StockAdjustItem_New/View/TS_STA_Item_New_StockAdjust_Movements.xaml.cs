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

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_STA_Item_New_StockAdjust_Movements.xaml
    /// </summary>
    public partial class TS_STA_Item_New_StockAdjust_Movements : Page
    {
        public TS_STA_Item_New_StockAdjust_Movements(int num)
        {
            InitializeComponent();

            if(GetController().movementsView.movements.Count > 0)
            {
                BT_StockAdjustSave.IsEnabled = true;
            }
        }

        private void EV_StoredStock_Reduce(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoredStock_Reduce();
        }

        private void EV_StoredStock_Increase(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoredStock_Increase();
        }

        private void EV_StockAdjustSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewStockAdjust();
        }

        private Controller.CT_STA_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STA_Item_New)a.MainFrame.Content;
        }
    }
}
