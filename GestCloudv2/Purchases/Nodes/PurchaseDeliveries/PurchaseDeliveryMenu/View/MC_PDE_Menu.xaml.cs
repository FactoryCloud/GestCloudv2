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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.View
{
    /// <summary>
    /// Interaction logic for MC_PUR_Menu.xaml
    /// </summary>
    public partial class MC_PDE_Menu : Page
    {
        public MC_PDE_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_PurchaseDeliveries.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_PurchaseDeliveries.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().stockAdjust != null)
            {
                DG_PurchaseDeliveries.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_PurchaseDeliveryLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_PurchaseDeliveries.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_PurchaseDeliveries.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetPurchaseDelivery(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_PurchaseDeliveries.ItemsSource = null;
            DG_PurchaseDeliveries.ItemsSource = GetController().purchaseDeliveriesView.GetTable();
        }

        private Controller.CT_PurchaseDeliveryMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PurchaseDeliveryMenu)a.MainFrame.Content;
        }
    }
}
