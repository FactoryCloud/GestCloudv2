using FrameworkDB.V1;
using FrameworkView.V1;
using GestCloudv2.Documents.DCM_Transfers.Controller;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleDeliveries.SaleDeliveryMenu.Controller
{
    public partial class CT_SaleDeliveryMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public SaleDelivery saleDelivery;

        public CT_SaleDeliveryMenu()
        {
            itemsView = new SaleDeliveriesView();
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
            return new SaleDeliveryItem.SaleDeliveryItem_New.Controller.CT_SDE_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new SaleDeliveryItem.SaleDeliveryItem_Load.Controller.CT_SDE_Item_Load(saleDelivery, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new SaleDeliveryItem.SaleDeliveryItem_Load.Controller.CT_SDE_Item_Load(saleDelivery, 1);
        }

        public override CT_DCM_Transfers SetInvoiceTransfer()
        {
            return new SaleDeliveries.SaleDeliveryTransfer.Controller.CT_SDE_Transfer();
        }

        override public bool SelectedItem()
        {
            return saleDelivery != null;
        }

        override public void SetItem(int num)
        {
            saleDelivery = db.SaleDeliveries.Where(c => c.SaleDeliveryID== num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

