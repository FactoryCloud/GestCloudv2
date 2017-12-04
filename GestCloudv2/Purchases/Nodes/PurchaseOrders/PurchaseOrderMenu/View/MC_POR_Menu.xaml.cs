using System;
using System.Collections.Generic;
using System.Data;
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

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.View
{
    /// <summary>
    /// Interaction logic for MC_PUR_Menu.xaml
    /// </summary>
    public partial class MC_POR_Menu : Page
    {
        public MC_POR_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_PurchaseOrders.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_PurchaseOrders.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().stockAdjust != null)
            {
                DG_PurchaseOrders.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_StockAdjustLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_PurchaseOrders.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_PurchaseOrders.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetPurchaseOrder(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_PurchaseOrders.ItemsSource = null;
            DG_PurchaseOrders.ItemsSource = GetController().purchaseOrdersView.GetTable();
        }

        private Controller.CT_PurchaseOrderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseOrderMenu)a.MainFrame.Content;
        }
    }
}
