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
    public class StockAdjustsView
    {
        List<Company> companies { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public StockAdjustsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
        }

        public void UpdateTable()
        {
            companies = db.Companies.ToList();

            dt.Clear();
            foreach(Company comp in companies)
            {
                dt.Rows.Add(comp.CompanyID, comp.Name);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
