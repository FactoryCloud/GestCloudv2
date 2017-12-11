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

namespace GestCloudv2.PurchasesDelivery.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    /// <summary>
    /// Interaction logic for TS_POR_Item_New_PurchaseOrder_Movements.xaml
    /// </summary>
    public partial class TS_PDE_Item_New_PurchaseDelivery_Movements : Page
    {
        public TS_PDE_Item_New_PurchaseDelivery_Movements(int num)
        {
            InitializeComponent();

            if(GetController().movementSelected != null)
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

        private void EV_StoredStock_Increase(object sender, RoutedEventArgs e)
        {
            GetController().MD_StoredStock_Increase();
        }

        private void EV_StockAdjustSave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewStockAdjust();
        }

        private Controller.CT_PDE_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}
