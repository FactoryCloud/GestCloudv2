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

namespace GestCloudv2.PurchasesDelivery.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_POR_Item_New_PurchaseOrder_Movements.xaml
    /// </summary>
    public partial class MC_PDE_Item_New_PurchaseDelivery_Movements : Page
    {
        public MC_PDE_Item_New_PurchaseDelivery_Movements()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);
            DG_Movements.MouseLeftButtonUp += new MouseButtonEventHandler(EV_MovementsSelect);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            GetController().EV_UpdateSubMenu(0);
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

        private Controller.CT_PDE_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}
