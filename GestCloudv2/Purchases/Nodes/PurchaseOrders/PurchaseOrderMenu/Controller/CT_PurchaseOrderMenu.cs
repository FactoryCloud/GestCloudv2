using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseOrders.PurchaseOrderMenu.Controller
{
    public partial class CT_PurchaseOrderMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public PurchaseOrder purchaseOrder;

        public CT_PurchaseOrderMenu()
        {
            itemsView = new PurchaseOrdersView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_POR_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_POR_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_POR_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Purchases.Controller.CT_Purchases();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new PurchaseOrderItem.PurchaseOrderItem_New.Controller.CT_POR_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new PurchaseOrderItem.PurchaseOrderItem_Load.Controller.CT_POR_Item_Load(purchaseOrder, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new PurchaseOrderItem.PurchaseOrderItem_Load.Controller.CT_POR_Item_Load(purchaseOrder, 1);
        }

        override public bool SelectedItem()
        {
            return purchaseOrder != null;
        }

        override public void SetItem(int num)
        {
            purchaseOrder = db.PurchaseOrders.Where(c => c.PurchaseOrderID == num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

