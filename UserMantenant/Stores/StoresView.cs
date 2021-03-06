﻿using System;
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
    public class StoresView
    {
        List<Store> stores { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public StoresView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
        }

        public void UpdateTable()
        {
            stores = db.Stores.ToList();

            dt.Clear();
            foreach(Store store in stores)
            {
                dt.Rows.Add(store.StoreID, store.Name);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
