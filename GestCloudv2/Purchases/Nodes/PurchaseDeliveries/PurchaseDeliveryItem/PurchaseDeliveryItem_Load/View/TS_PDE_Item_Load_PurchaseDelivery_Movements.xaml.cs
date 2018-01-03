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

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_Load.View
{
    /// <summary>
    /// Interaction logic for TS_STA_Item_New_StockAdjust_Movements.xaml
    /// </summary>
    public partial class TS_PDE_Item_Load_PurchaseDelivery_Movements : Page
    {
        public TS_PDE_Item_Load_PurchaseDelivery_Movements(int num)
        {
            InitializeComponent();

            if (GetController().movementSelected != null)
            {
                BT_MovementDelete.IsEnabled = true;
                if (GetController().movementSelected.documentType == null)
                {
                    BT_MovementEdit.IsEnabled = true;
                }
            }

            if (GetController().movementsView.movements.Count > 0)
            {
                BT_StockAdjustSave.IsEnabled = true;
            }
        }

        private void EV_MovementAdd(object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementAdd();
        }

        private void EV_MovementEdit(object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementEdit();
        }

        private void EV_MovementDelete (object sender, RoutedEventArgs e)
        {
            GetController().MD_MovementDelete();
        }

        private void EV_Save(object sender, RoutedEventArgs e)
        {
            GetController().SaveDocument();
        }

        private Controller.CT_PDE_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_Load)a.MainFrame.Content;
        }
    }
}
