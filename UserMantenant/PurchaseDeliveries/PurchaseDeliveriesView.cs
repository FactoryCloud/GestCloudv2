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
        public PurchaseDeliveriesView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            dt.Columns.Add("Importe", typeof(string));
        }

        override public void UpdateTable()
        {
            items = db.PurchaseDeliveries.Include(e => e.provider.entity).ToList();

            dt.Clear();
            foreach (PurchaseDelivery purchaseDelivery in items)
            {
                dt.Rows.Add(purchaseDelivery.PurchaseDeliveryID,purchaseDelivery.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", purchaseDelivery.Date)}",purchaseDelivery.PurchaseDeliveryFinalPrice);
            }
        }
    }
}
