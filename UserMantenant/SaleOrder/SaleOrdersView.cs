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
    public class SaleOrdersView
    {
        List<SaleOrder> saleOrders { get; set; }
        GestCloudDB db;
        private DataTable dt;

        public SaleOrdersView()
        {
            db = new GestCloudDB();
            dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fecha", typeof(string));
            //dt.Columns.Add("Almacén", typeof(string));
        }

        public void UpdateTable()
        {
            saleOrders = db.SaleOrders.Include(e => e.client.entity).ToList();

            dt.Clear();
            foreach (SaleOrder saleOrder in saleOrders)
            {
                dt.Rows.Add(saleOrder.SaleOrderID,saleOrder.client.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", saleOrder.Date)}");
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
