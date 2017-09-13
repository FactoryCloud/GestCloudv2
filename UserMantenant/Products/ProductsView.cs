using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FrameworkView.V1
{
    public class ProductsView
    {
        List<Product> products { get; set; }
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

        public void UpdateTable()
        {
            List<Product> products = db.Products.Include(p=> p.productType).OrderBy(u => u.Name).ToList();

            dt.Clear();
            foreach (Product item in products)
            {
                dt.Rows.Add(item.Name, item.productType.Name, item.Price);
            }
        }

        public void UpdateFilteredTable()
        {
            List<Product> products = new List<Product>();
            if (productType.ProductTypeID > 0)
            {
                switch(productType.ProductTypeID)
                {
                    case 1:
                        if (expansion.ExpansionID > 0)
                        {
                            List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.EnName).ToList();

                            foreach (MTGCard item in cards)
                            {
                                Product temp = db.Products.First(p => p.ExternalID == item.ProductID);
                                products.Add(temp);
                            }
                        }
                        break;
                }

                products.OrderBy(p => p.Name);

                dt.Clear();
                foreach (Product item in products)
                {
                    dt.Rows.Add(item.Name, item.productType.Name, item.Price);
                }
            }
        }

        private Boolean CardFilterExpansion(MTGCard card)
        {
            return card.ExpansionID == expansion.Id;
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
