using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestCloudv2.Sales.Nodes.SaleOrders.SaleOrderMenu.Controller
{
    public partial class CT_SaleOrderMenu : Documents.DCM_Menu.Controller.CT_DCM_Menu
    {
        public SaleOrder saleOrder;

        public CT_SaleOrderMenu()
        {
            itemsView = new SaleOrdersView();
        }

        public override void SetMC(int i)
        {
            MC_Page = new View.MC_SOR_Menu();
        }

        public override void SetTS()
        {
            TS_Page = new View.TS_SOR_Menu();
        }

        public override void SetNV()
        {
            NV_Page = new View.NV_SOR_Menu();
        }

        public override Main.Controller.CT_Common SetItemOriginal()
        {
            return new Sales.Controller.CT_Sales();
        }

        public override Documents.DCM_Items.DCM_Item_New.Controller.CT_DCM_Item_New SetItemNew()
        {
            return new SaleOrderItem.SaleOrderItem_New.Controller.CT_SOR_Item_New();
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoad()
        {
            return new SaleOrderItem.SaleOrderItem_Load.Controller.CT_SOR_Item_Load(saleOrder, 0);
        }

        public override Documents.DCM_Items.DCM_Item_Load.Controller.CT_DCM_Item_Load SetItemLoadEditable()
        {
            return new SaleOrderItem.SaleOrderItem_Load.Controller.CT_SOR_Item_Load(saleOrder, 1);
        }

        public override Documents.DCM_Transfers.Controller.CT_DCM_Transfers SetInvoiceTransfer()
        {
            return new Sales.Nodes.SaleOrders.SaleOrderTransfer.SOR_Transfer_Invoice.Controller.CT_SOR_Transfer_Invoice();
        }

        public override Documents.DCM_Transfers.Controller.CT_DCM_Transfers SetDeliveryTransfer()
        {
            return new Sales.Nodes.SaleOrders.SaleOrderTransfer.SOR_Transfer_Delivery.Controller.CT_SOR_Transfer_Delivery();
        }

        override public bool SelectedItem()
        {
            return saleOrder != null;
        }

        public override bool IsEditable()
        {
            return saleOrder.SaleDeliveryID == null && saleOrder.SaleInvoiceID == null;
        }

        override public void SetItem(int num)
        {
            saleOrder = db.SaleOrders.Where(c => c.SaleOrderID== num).Include(c => c.company).First();
            base.SetItem(num);
        }
    }
}

