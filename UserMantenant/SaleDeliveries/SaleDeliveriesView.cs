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
    public class SaleDeliveriesView: ItemsView
    {
        List<SaleDelivery> items;
        List<SaleDelivery> itemsRemoved;
        int Option { get; set; }
        Client client;

        public SaleDeliveriesView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));

            Option = 0;
        }

        public SaleDeliveriesView(Client client) : this()
        {
            this.client = client;
        }

        public SaleDeliveriesView(List<SaleDelivery> Documents) : this()
        {
            items = Documents;
            Option = 1;
        }

        public SaleDeliveriesView(List<SaleDelivery> Documents, Client client) : this()
        {
            itemsRemoved = Documents;

            if (client.ClientID > 0)
                this.client = client;

            Option = 2;
        }

        override public void UpdateTable()
        {
            if (Option == 0)
            {
                if (client != null)
                    items = db.SaleDeliveries.Where(p => p.ClientID == client.ClientID).Include(e => e.client.entity).ToList();

                else
                    items = db.SaleDeliveries.Include(e => e.client.entity).ToList();
            }

            if (Option == 2)
            {
                if (client != null)
                    items = db.SaleDeliveries.Where(p => p.ClientID == client.ClientID && p.SaleInvoiceID == null).Include(p => p.client.entity).ToList();

                else
                    items = db.SaleDeliveries.Where(p => p.SaleInvoiceID == null).Include(e => e.client.entity).ToList();

                foreach (SaleDelivery item in itemsRemoved)
                {
                    items.Remove(items.Where(i => i.SaleDeliveryID == item.SaleDeliveryID).First());
                }
            }

            dt.Clear();
            foreach (SaleDelivery saleDelivery in items)
            {
                dt.Rows.Add(saleDelivery.SaleDeliveryID, saleDelivery.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleDelivery.Date)}",saleDelivery.SaleDeliveryFinalPrice);
            }
        }
    }
}
