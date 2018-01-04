using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Purchases.Nodes.PurchaseInvoices.PurchaseInvoiceMenu.Controller
{
    public partial class CT_PurchaseInvoiceMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public PurchaseInvoice purchaseInvoice;

        public CT_PurchaseInvoiceMenu()
        {
            itemsView = new PurchaseInvoicesView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_PIN_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_PIN_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_PIN_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Purchases.Controller.CT_Purchases();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new PurchaseInvoiceItem.PurchaseInvoiceItem_New.Controller.CT_PIN_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new PurchaseInvoiceItem.PurchaseInvoiceItem_Load.Controller.CT_PIN_Item_Load(purchaseInvoice, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new PurchaseInvoiceItem.PurchaseInvoiceItem_Load.Controller.CT_PIN_Item_Load(purchaseInvoice, 1);
        }

        override public bool SelectedItem()
        {
            return purchaseInvoice != null;
        }

        override public void SetItem(int num)
        {
            purchaseInvoice = db.PurchaseInvoices.Where(c => c.PurchaseInvoiceID == num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

