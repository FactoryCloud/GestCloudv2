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

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderMenu.View
{
    /// <summary>
    /// Interaction logic for MC_SAL_Menu.xaml
    /// </summary>
    public partial class MC_SOR_Menu : Page
    {
        public MC_SOR_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_SaleAdjusts.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_SaleAdjusts.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().stockAdjust != null)
            {
                DG_SaleAdjusts.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_SaleOrderLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_SaleAdjusts.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_SaleAdjusts.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetSaleOrder(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_SaleAdjusts.ItemsSource = null;
            DG_SaleAdjusts.ItemsSource = GetController().saleOrdersView.GetTable();
        }

        private Controller.CT_SaleOrderMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_SaleOrderMenu)a.MainFrame.Content;
        }
    }
}
