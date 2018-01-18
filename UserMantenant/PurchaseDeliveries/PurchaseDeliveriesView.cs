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
    public class PurchaseDeliveriesView: ItemsView
    {
        List<PurchaseDelivery> items;
        List<PurchaseDelivery> itemsRemoved;
        int Option { get; set; }
        Provider provider;

        public PurchaseDeliveriesView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));

            Option = 0;
        }

        public PurchaseDeliveriesView(List<PurchaseDelivery> Documents) : this()
        {
            items = Documents;
            Option = 1;
        }

        public PurchaseDeliveriesView(List<PurchaseDelivery> Documents, Provider provider) : this()
        {
            itemsRemoved = Documents;

            if(provider.ProviderID > 0)
                this.provider = provider;

            Option = 2;
        }

        override public void UpdateTable()
        {
            if(Option == 0)
                items = db.PurchaseDeliveries.Include(e => e.provider.entity).ToList();

            if(Option == 2)
            {
                if (provider != null)
                    items = db.PurchaseDeliveries.Where(p => p.ProviderID == provider.ProviderID && p.PurchaseInvoiceID == null).Include(p => p.provider.entity).ToList();

                else
                    items = db.PurchaseDeliveries.Where(p => p.PurchaseInvoiceID == null).Include(e => e.provider.entity).ToList();

                foreach (PurchaseDelivery item in itemsRemoved)
                {
                    items.Remove(items.Where(i => i.PurchaseDeliveryID == item.PurchaseDeliveryID).First());
                }
            }

            dt.Clear();
            foreach (PurchaseDelivery purchaseDelivery in items)
            {
                dt.Rows.Add(purchaseDelivery.PurchaseDeliveryID,purchaseDelivery.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", purchaseDelivery.Date)}",purchaseDelivery.PurchaseDeliveryFinalPrice);
            }
        }

        public void SetDocuments(List<PurchaseDelivery> Documents)
        {
            items = Documents;
        }

        public PurchaseDelivery GetPurchaseDelivery(int num)
        {
            return db.PurchaseDeliveries.Where(p => p.PurchaseDeliveryID == num).First();
        }
    }
}
