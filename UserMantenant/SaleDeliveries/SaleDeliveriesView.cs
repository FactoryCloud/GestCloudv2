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
        public SaleDeliveriesView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
        }

        override public void UpdateTable()
        {
            items = db.SaleDeliveries.Include(e => e.client.entity).ToList();

            dt.Clear();
            foreach (SaleDelivery saleDelivery in items)
            {
                dt.Rows.Add(saleDelivery.SaleDeliveryID, saleDelivery.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleDelivery.Date)}");
            }
        }
    }
}
