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
using FrameworkDB.V1;

namespace GestCloudv2.Stocks.Nodes.StockAdjusts.StockAdjustItem.StockAdjustItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_STA_Item_New_StockAdjust_Movements.xaml
    /// </summary>
    public partial class MC_STA_Item_New_StockAdjust_Movements : Page
    {
        public MC_STA_Item_New_StockAdjust_Movements()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        public void UpdateData()
        {
            DG_Movements.ItemsSource = null;
            DG_Movements.ItemsSource = GetController().movementsView.GetTable();
        }

        private Controller.CT_STA_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STA_Item_New)a.MainFrame.Content;
        }
    }
}
