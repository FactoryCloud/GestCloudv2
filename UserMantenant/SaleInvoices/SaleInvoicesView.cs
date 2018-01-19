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
    public class SaleInvoicesView : ItemsView
    {
        List<SaleInvoice> items;

        public Client client;

        public SaleInvoicesView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));
        }

        public SaleInvoicesView(Client client) : this()
        {
            this.client = client;
        }

        override public void UpdateTable()
        {
            if (client != null)
                items = db.SaleInvoices.Where(p => p.ClientID == client.ClientID).Include(e => e.client.entity).ToList();

            else
                items = db.SaleInvoices.Include(e => e.client.entity).ToList();

            dt.Clear();
            foreach (SaleInvoice saleInvoice in items)
            {
                dt.Rows.Add(saleInvoice.SaleInvoiceID, saleInvoice.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleInvoice.Date)}",saleInvoice.SaleInvoiceFinalPrice);
            }
        }
    }
}
