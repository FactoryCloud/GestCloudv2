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
using System.Data;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderItem.PurchaseOrderItem_Load.View
{ 
    /// <summary>
    /// Interaction logic for MC_STA_Item_New_StockAdjust_Movements.xaml
    /// </summary>
    public partial class MC_POR_Item_Load_PurchaseOrder_Movements : Page 
    {
        public MC_POR_Item_Load_PurchaseOrder_Movements()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_Movements.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MovementsSelect);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        public void EV_MovementsSelect(object sender, RoutedEventArgs e)
        {
            int movement = DG_Movements.SelectedIndex;

            if (movement >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Movements.ItemContainerGenerator.ContainerFromIndex(movement);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetMovementSelected(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        public void UpdateData()
        {
            DG_Movements.ItemsSource = null;
            DG_Movements.ItemsSource = GetController().movementsView.GetTable();
        }

        private Controller.CT_POR_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_POR_Item_Load)a.MainFrame.Content;
        }
    }
}
