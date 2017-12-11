using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseDeliveries.PurchaseDeliveryMenu.Controller
{
    public partial class CT_PurchaseDeliveryMenu : Main.Controller.CT_Common
    {
        public PurchaseDeliveriesView purchaseDeliveriesView;
        public PurchaseDelivery purchaseDelivery;
        public StockAdjust stockAdjust;

        public CT_PurchaseDeliveryMenu()
        {
            purchaseDeliveriesView = new PurchaseDeliveriesView();
        }

        public void SetPurchaseDelivery(int num)
        {
            purchaseDelivery = db.PurchaseDeliveries.Where(c => c.PurchaseDeliveryID== num).Include(c => c.company).First();
            TS_Page = new View.TS_PDE_Menu();
            LeftSide.Content = TS_Page;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void EV_CT_DeliveryPurchaseNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void EV_CT_PurchaseDeliveryLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void EV_CT_PurchaseDeliveryLoadEditable()
        {
            Information["controller"] = 3;
            ChangeController();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        public override void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_PDE_Menu();
                    TS_Page = new View.TS_PDE_Menu();
                    MC_Page = new View.MC_PDE_Menu();
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Purchases.Controller.CT_Purchases();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new PurchaseDeliveryItem.PurchaseDeliveryItem_New.Controller.CT_PDE_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new PurchaseDeliveryItem.PurchaseDeliveryItem_Load.Controller.CT_PDE_Item_Load(purchaseDelivery,0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new PurchaseDeliveryItem.PurchaseDeliveryItem_Load.Controller.CT_PDE_Item_Load(purchaseDelivery, 1);
                    break;
            
            }

        }
    }
}

