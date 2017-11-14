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
        List<StockAdjust> stockAdjusts { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public StockAdjustsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Fecha", typeof(string));
        }

        public void UpdateTable()
        {
            stockAdjusts = db.StockAdjusts.ToList();

            dt.Clear();
            foreach(StockAdjust stockAdjust in stockAdjusts)
            {
                dt.Rows.Add(stockAdjust.StockAdjustID, $"{String.Format("{0:dd/MM/yyyy}", stockAdjust.Date)}" );
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
