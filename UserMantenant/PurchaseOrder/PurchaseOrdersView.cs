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
    public class PurchaseOrdersView
    {
        List<PurchaseOrder> purchaseOrders { get; set; }
        GestCloudDB db;
        private DataTable dt;

        public PurchaseOrdersView()
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
            purchaseOrders = db.PurchaseOrders.Include(e => e.provider.entity).ToList();

            dt.Clear();
            foreach (PurchaseOrder purchaseOrder in purchaseOrders)
            {
                dt.Rows.Add(purchaseOrder.PurchaseOrderID,purchaseOrder.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", purchaseOrder.Date)}");
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
