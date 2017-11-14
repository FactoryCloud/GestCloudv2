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
    public class StoredStocksView
    {
        List<Movement> movements { get; set; }
        public string ProductName { get; set; }
        public GestCloudDB db;

        int option;
        public ProductType productType;
        public Expansion expansion;
        public Store store;
        public Product products;
        private DataTable dt;

        public StoredStocksView(int option)
        {
            db = new GestCloudDB();
            store = new Store();
            productType = new ProductType();
            expansion = new Expansion();

            this.option = option;

            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Tipo Producto", typeof(string));
            dt.Columns.Add("Cantidad", typeof(decimal));
        }

        public void SetExpansion(int num)
        {
            expansion = db.Expansions.First(ex => ex.Id == num);
        }

        public void SetProductType(int num)
        {
            productType = db.ProductTypes.First(ex => ex.ProductTypeID == num);
        }

        public void SetStore(int num)
        {
            store = db.Stores.First(ex => ex.StoreID == num);
        }

        public List<Expansion> GetExpansions()
        {
            List<Expansion> expansions = db.Expansions.OrderByDescending(ex => ex.ReleaseDate).ToList();
            return expansions;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = db.Products.OrderByDescending(ex => ex.ProductID).ToList();
            return products;
        }

        public List<ProductType> GetProductTypes()
        {
            List<ProductType> productTypes = db.ProductTypes.OrderByDescending(ex => ex.Name).ToList();
            return productTypes;
        }

        public List<Store> GetStores()
        {
            List<Store> stores = db.Stores.OrderBy(ex => ex.Code).ToList();
            return stores;
        }

        public void UpdateTable()
        {
            DocumentType documentTypeIn = db.DocumentTypes.Where(d => d.Name.Contains("StockAdjust") && d.Input == 1).First();
            DocumentType documentTypeOut = db.DocumentTypes.Where(d => d.Name.Contains("StockAdjust") && d.Input == 0).First();
            movements = db.Movements.Where(m => m.DocumentTypeID == documentTypeIn.DocumentTypeID || m.DocumentTypeID == documentTypeOut.DocumentTypeID).Include(m => m.product).Include(m => m.product.productType).OrderBy(u => u.product.Name).ToList();

            MessageBox.Show($"{movements.Count}");

            dt.Clear();
            foreach (Movement item in movements)
            {
                dt.Rows.Add(item.MovementID, item.product.Name, item.product.productType.Name, item.Quantity);
            }
        }

        public void UpdateFilteredTable()
        {
            /*List<Product> products = new List<Product>();

            if (productType.ProductTypeID > 0)
            {
                switch(productType.ProductTypeID)
                {
                    case 1:
                        List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.EnName).ToList();
                        //MessageBox.Show($"{cards.Count}");

                        foreach (MTGCard item in cards)
                        {
                            Product temp = db.Products.First(p => p.ExternalID == item.ProductID);
                            products.Add(temp);
                        }
                        break;
                }

                products.OrderBy(p => p.Name);

                dt.Clear();
                foreach (Product item in products)
                {
                    dt.Rows.Add(item.ProductID, item.Name, item.productType.Name, item.Price);
                }
            }*/
        }

        private Boolean CardFilterExpansion(MTGCard card)
        {
            /*if(ProductName != null && expansion.ExpansionID > 0)
            {
                return card.ExpansionID == expansion.Id && card.EnName.ToLower().Contains(ProductName.ToLower());
            }

            else if (ProductName != null)
            {               
                return card.EnName.ToLower().Contains(ProductName.ToLower());
            }

            else if(ProductName == null && expansion.ExpansionID>0)
            {
                return card.ExpansionID == expansion.Id;
            }

            else
            {
                return false;
            }*/
            return false;
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFiltered()
        {
            UpdateFilteredTable();
            return dt.DefaultView;
        }
    }
}
