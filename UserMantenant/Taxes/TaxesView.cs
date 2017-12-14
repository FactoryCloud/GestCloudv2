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
    public class TaxesView
    {
        List<Tax> taxes { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public TaxesView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("Tipo", typeof(int));
            dt.Columns.Add("Porcetanje", typeof(decimal));
        }

        public void UpdateTable()
        {
            taxes = db.Taxes.ToList();

            dt.Clear();
            foreach(Tax tax in taxes)
            {
                dt.Rows.Add(tax.TaxID, tax.Percentage);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
