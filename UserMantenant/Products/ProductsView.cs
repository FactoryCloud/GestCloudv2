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

        int option;
        public ProductType productType;
        public Expansion expansion;
        private DataTable dt;

        public ProductsView(int option)
        {
            db = new GestCloudDB();
            dt = new DataTable();
            productType = new ProductType();
            ProductName = "";

            this.option = option;

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

        public FrameworkDB.V1.Condition GetCondition(int num)
        {
            return db.Conditions.Where(ex => ex.ConditionID == num).First();
        }

        public Product GetProduct(int num)
        {
            Product product = db.Products.Where(ex => ex.ProductID == num).Include(pt => pt.productType).First();
            return product;
        }

        public List<FrameworkDB.V1.Condition> GetConditions()
        {
            List<FrameworkDB.V1.Condition> conditions = db.Conditions.OrderBy(ex => ex.ConditionID).ToList();
            return conditions;
        }

        public void UpdateTable()
        {
            List<Product> products = new List<Product>();
            if (productType.ProductTypeID > 0)
            {
                switch(productType.ProductTypeID)
                {
                    case 1:
                        if (expansion != null)
                            products = db.Products.Where(pr => pr.Name.ToLower()
                                .Contains($"({expansion.Abbreviation.ToLower()})") &&
                                pr.Name.ToLower().Contains(ProductName.ToLower())).ToList();

                        else
                            products = db.Products.Where(pr => 
                                pr.Name.ToLower().Contains(ProductName.ToLower())).ToList();
                        /*else
                        {
                            List<MTGCard> cards = CardFilter();
                            if (cards != null)
                            {
                                foreach (MTGCard item in cards)
                                {
                                    Product temp = db.Products.First(p => p.ExternalID == item.ProductID);
                                    products.Add(temp);
                                }
                            }
                        }*/
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

        private List<MTGCard> CardFilter()
        {
            if (ProductName.Length == 0 && expansion == null)
            {
                return null;
            }

            else if (ProductName.Length > 0 && expansion == null)
            {
                return db.MTGCards.Where(c => c.EnName.ToLower().Contains(ProductName.ToLower())).ToList();
            }

            else if (ProductName.Length > 0 && expansion.ExpansionID > 0)
            {
                return db.MTGCards.Where(c => c.ExpansionID == expansion.Id && c.EnName.ToLower().Contains(ProductName.ToLower())).ToList();
            }

            else if (ProductName.Length == 0 && expansion.ExpansionID > 0)
            {
                return db.MTGCards.Where(c => c.ExpansionID == expansion.Id).ToList();
            }

            else
                return null;
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
