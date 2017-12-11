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
    public class PurchaseDeliveriesView
    {
        List<PurchaseDelivery> purchaseDeliveries { get; set; }
        GestCloudDB db;
        private DataTable dt;

        public PurchaseDeliveriesView()
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
            purchaseDeliveries = db.PurchaseDeliveries.Include(e => e.provider.entity).ToList();

            dt.Clear();
            foreach (PurchaseDelivery purchaseDelivery in purchaseDeliveries)
            {
                dt.Rows.Add(purchaseDelivery.PurchaseDeliveryID,purchaseDelivery.provider.entity.Name, $"{String.Format("{0:dd/MM/yyyy}", purchaseDelivery.Date)}");
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
