using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows;

namespace FrameworkView.V1
{
    public class PaymentMethodsView
    {
        List<PaymentMethod> paymentMethods { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public PaymentMethodsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
        }

        public void UpdateTable()
        {
            paymentMethods = db.PaymentMethods.ToList();

            dt.Clear();
            foreach(PaymentMethod paymentMethod in paymentMethods)
            {
                dt.Rows.Add(paymentMethod.PaymentMethodID, paymentMethod.Name);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
