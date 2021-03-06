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
    public class ProductTypesView
    {
        List<ProductType> productTypes { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public ProductTypesView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
        }

        public void UpdateTable()
        {
            productTypes = db.ProductTypes.ToList();

            dt.Clear();
            foreach(ProductType productType in productTypes)
            {
                dt.Rows.Add(productType.ProductTypeID, productType.Name);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
