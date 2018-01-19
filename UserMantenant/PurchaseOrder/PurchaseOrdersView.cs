using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class PurchaseOrdersView:ItemsView
    {
        List<PurchaseOrder> items;
        List<PurchaseOrder> itemsRemoved;
        int Option { get; set; }
        Provider provider;

        public PurchaseOrdersView()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));
        }

        public PurchaseOrdersView(Provider provider) : this()
        {
            this.provider = provider;
        }

        public PurchaseOrdersView(List<PurchaseOrder> Documents) : this()
        {
            items = Documents;
            Option = 1;
        }

        public PurchaseOrdersView(List<PurchaseOrder> Documents, Provider provider) : this()
        {
            itemsRemoved = Documents;

            if (provider.ProviderID > 0)
                this.provider = provider;

            Option = 2;
        }

        override public void UpdateTable()
        {
            if (Option == 0)
                items = db.PurchaseOrders.Include(e => e.provider.entity).ToList();

            if (Option == 2)
            {
                if (provider != null)
                    items = db.PurchaseOrders.Where(p => p.ProviderID == provider.ProviderID && p.PurchaseInvoiceID == null && p.PurchaseDeliveryID == null).Include(p => p.provider.entity).ToList();

                else
                    items = db.PurchaseOrders.Where(p => p.PurchaseInvoiceID == null && p.PurchaseDeliveryID == null).Include(e => e.provider.entity).ToList();

                foreach (PurchaseOrder item in itemsRemoved)
                {
                    items.Remove(items.Where(i => i.PurchaseOrderID == item.PurchaseOrderID).First());
                }
            }

            dt.Clear();
            foreach (PurchaseOrder purchaseOrder in items)
            {
                dt.Rows.Add(purchaseOrder.PurchaseOrderID,purchaseOrder.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", purchaseOrder.Date)}",purchaseOrder.PurchaseOrderFinalPrice);
            }
        }
    }
}
