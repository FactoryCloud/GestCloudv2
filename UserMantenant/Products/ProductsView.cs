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

        //int option;
        public Product product;
        public FrameworkDB.V1.Condition condition;
        public ProductType productType;
        public Expansion expansion;
        public bool Altered;
        public bool Signed;
        public bool Foil;
        public bool Playset;
        public decimal Quantity;
        public decimal PurchasePrice;
        public decimal SalePrice;
        private DataTable dt;

        public ProductsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            //product = new Product();
            productType = new ProductType();
            ProductName = "";

            //this.option = option;

            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio de compra", typeof(decimal));
            dt.Columns.Add("Precio de venta", typeof(decimal));
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
            return db.Expansions.OrderByDescending(ex => ex.ReleaseDate).ToList();
        }

        public Expansion GetExpansion(int num)
        {
            return db.MTGCards.Where(c => c.ProductID == num).First().expansion;
        }

        public List<ProductType> GetProductTypes()
        {
            return db.ProductTypes.OrderByDescending(ex => ex.Name).ToList();
        }

        public FrameworkDB.V1.Condition GetCondition(int num)
        {
            return db.Conditions.Where(ex => ex.ConditionID == num).First();
        }

        public Product GetProduct(int num)
        {
            return db.Products.Where(ex => ex.ProductID == num).Include(pt => pt.productType).First();
        }

        public List<FrameworkDB.V1.Condition> GetConditions()
        {
            return db.Conditions.OrderBy(ex => ex.ConditionID).ToList();
        }

        public Movement UpdateMovement(Movement movement)
        {
            movement.product = product;
            movement.condition = condition;
            movement.Quantity = Convert.ToDecimal(Quantity);
            movement.IsAltered = Convert.ToInt16(Altered);
            movement.IsFoil = Convert.ToInt16(Foil);
            movement.IsPlayset = Convert.ToInt16(Playset);
            movement.IsSigned = Convert.ToInt16(Signed);
            movement.PurchasePrice = Convert.ToDecimal(PurchasePrice);
            movement.SalePrice = Convert.ToDecimal(SalePrice);

            return movement;
        }

        /*public void UpdateTable()
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
        /*                break;
                }

                products.OrderBy(p => p.Name);
                dt.Clear();
                
                foreach (Product item in products)
                {
                    dt.Rows.Add(item.ProductID, item.Name, item.productType.Name, item.Price);
                }
            }
        }*/

        public void UpdateTable()
        {
            products = db.Products.ToList();

            dt.Clear();
            foreach (Product product in products)
            {
                dt.Rows.Add(product.ProductID, product.Name, product.PurchasePrice1, product.SalePrice1);
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
