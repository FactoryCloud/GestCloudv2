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
    public class ProductsView
    {
        List<Product> products { get; set; }
        public string ProductName { get; set; }
        public GestCloudDB db;

        public ProductType productType;
        public Expansion expansion;
        private DataTable dt;

        public ProductsView()
        {
            db = new GestCloudDB();

            dt = new DataTable();
            productType = new ProductType();
            //productType = db.ProductTypes.First(p => p.Name == "MTGCard");
            expansion = new Expansion();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Tipo Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
        }

        public void SetExpansion(int num)
        {
            expansion = db.Expansions.First(ex => ex.Id == num);
        }

        public void SetProductType(int num)
        {
            productType = db.ProductTypes.First(ex => ex.ProductTypeID == num);
        }

        public List<Expansion> GetExpansions()
        {
            List<Expansion> expansions = db.Expansions.OrderByDescending(ex => ex.ReleaseDate).ToList();
            return expansions;
        }

        public List<ProductType> GetProductTypes()
        {
            List<ProductType> productTypes = db.ProductTypes.OrderByDescending(ex => ex.Name).ToList();
            return productTypes;
        }

        public List<FrameworkDB.V1.Condition> GetConditions()
        {
            List<FrameworkDB.V1.Condition> conditions = db.Conditions.OrderBy(ex => ex.ConditionID).ToList();
            return conditions;
        }

        public void UpdateTable()
        {
            MessageBox.Show("prueba");
            List<Product> products = db.Products.Include(p=> p.productType).OrderBy(u => u.Name).ToList();
            db.UpdatePricesMKM(products);

            products = db.Products.Include(p => p.productType).OrderBy(u => u.Name).ToList();
            dt.Clear();
            foreach (Product item in products)
            {
                dt.Rows.Add(item.ProductID, item.Name, item.productType.Name, item.Price);
            }
        }

        public void UpdateFilteredTable()
        {
            List<Product> products = new List<Product>();
            /*if (productType.ProductTypeID > 0)
            {
                switch (productType.ProductTypeID)
                {
                    case 1:
                        List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.EnName).ToList();
                        //MessageBox.Show($"{cards.Count}");

                        foreach (MTGCard item in cards.Take(300))
                        {
                            Product temp = db.Products.First(p => p.ExternalID == item.ProductID);
                            products.Add(temp);
                        }
                        break;
                }
                db.UpdatePricesMKM(products);
            }*/

            if (productType.ProductTypeID > 0)
            {
                switch(productType.ProductTypeID)
                {
                    case 1:
                        List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.EnName).ToList();
                        //MessageBox.Show($"{cards.Count}");

                        foreach (MTGCard item in cards.Take(300))
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
            }
        }

        private Boolean CardFilterExpansion(MTGCard card)
        {
            if(ProductName != null && expansion.ExpansionID > 0)
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
            }
        }

        public IEnumerable GetTable()
        {
            //UpdateTable();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFiltered()
        {
            UpdateFilteredTable();
            return dt.DefaultView;
        }
    }
}
