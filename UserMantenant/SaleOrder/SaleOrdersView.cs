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
        public SaleOrdersView():base()
        {
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
        }

        override public void UpdateTable()
        {
            items = db.SaleOrders.Include(e => e.client.entity).ToList();

            dt.Clear();
            foreach (SaleOrder saleOrder in items)
            {
                dt.Rows.Add(saleOrder.SaleOrderID, saleOrder.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleOrder.Date)}");
            }
        }
    }
}
