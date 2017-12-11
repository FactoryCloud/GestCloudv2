using FrameworkDB.V1;
using FrameworkView.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestCloudv2.PurchasesDelivery.Nodes.PurchaseDeliveries.PurchaseDeliveryItem.PurchaseDeliveryItem_New.View
{
    public partial class FW_PDE_Item_New_SelectProvider : FloatWindows.ProviderSelectWindow
    {
        public FW_PDE_Item_New_SelectProvider() : base()
        {
            
        }

        override public Main.Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (PurchaseDeliveryItem_New.Controller.CT_PDE_Item_New)a.MainFrame.Content;
        }
    }
}
