using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleInvoices.SaleInvoiceMenu.Controller
{
    public partial class CT_SaleInvoiceMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public SaleInvoice saleInvoice;

        public CT_SaleInvoiceMenu()
        {
            itemsView = new SaleInvoicesView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_SDE_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SDE_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SDE_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Sales.Controller.CT_Sales();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new SaleInvoiceItem.SaleInvoiceItem_New.Controller.CT_SIN_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new SaleInvoiceItem.SaleInvoiceItem_Load.Controller.CT_SIN_Item_Load(saleInvoice, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new SaleInvoiceItem.SaleInvoiceItem_Load.Controller.CT_SIN_Item_Load(saleInvoice, 1);
        }

        override public bool SelectedItem()
        {
            return saleInvoice != null;
        }

        override public void SetItem(int num)
        {
            saleInvoice = db.SaleInvoices.Where(c => c.SaleInvoiceID== num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

