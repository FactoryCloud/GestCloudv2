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
    public partial class CT_PurchaseDeliveryMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public PurchaseDelivery purchaseDelivery;

        public CT_PurchaseDeliveryMenu()
        {
            itemsView = new PurchaseDeliveriesView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_PDE_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PDE_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PDE_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Purchases.Controller.CT_Purchases();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new PurchaseDeliveryItem.PurchaseDeliveryItem_New.Controller.CT_PDE_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new PurchaseDeliveryItem.PurchaseDeliveryItem_Load.Controller.CT_PDE_Item_Load(purchaseDelivery, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new PurchaseDeliveryItem.PurchaseDeliveryItem_Load.Controller.CT_PDE_Item_Load(purchaseDelivery, 1);
        }

        public override Documents.DCM_Transfers.Controller.CT_DCM_Transfers SetInvoiceTransfer()
        {
            return new PurchaseDeliveryTransfer.Controller.CT_PDE_Transfer();
        }

        override public bool SelectedItem()
        {
            return purchaseDelivery != null;
        }

        override public void SetItem(int num)
        {
            purchaseDelivery = db.PurchaseDeliveries.Where(c => c.PurchaseDeliveryID== num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

