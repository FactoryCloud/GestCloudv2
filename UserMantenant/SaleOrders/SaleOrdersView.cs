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
    public class SaleOrdersView : ItemsView
    {
        List<SaleOrder> items;
        List<SaleOrder> itemsRemoved;
        int Option { get; set; }
        Client client;

        public SaleOrdersView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));
        }

        public SaleOrdersView(Client client) : this()
        {
            this.client = client;
        }

        public SaleOrdersView(List<SaleOrder> Documents) : this()
        {
            items = Documents;
            Option = 1;
        }

        public SaleOrdersView(List<SaleOrder> Documents, Client client) : this()
        {
            itemsRemoved = Documents;

            if (client.ClientID > 0)
                this.client = client;

            Option = 2;
        }

        override public void UpdateTable()
        {
            if (Option == 0)
                items = db.SaleOrders.Include(e => e.client.entity).ToList();

            if (Option == 2)
            {
                if (client != null)
                    items = db.SaleOrders.Where(p => p.ClientID == client.ClientID && p.SaleInvoiceID == null && p.SaleDeliveryID == null).Include(p => p.client.entity).ToList();

                else
                    items = db.SaleOrders.Where(p => p.SaleInvoiceID == null && p.SaleDeliveryID == null).Include(e => e.client.entity).ToList();

                foreach (SaleOrder item in itemsRemoved)
                {
                    items.Remove(items.Where(i => i.SaleOrderID == item.SaleOrderID).First());
                }
            }

            dt.Clear();
            foreach (SaleOrder saleOrder in items)
            {
                dt.Rows.Add(saleOrder.SaleOrderID, saleOrder.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleOrder.Date)}",saleOrder.SaleOrderFinalPrice);
            }
        }
    }
}
