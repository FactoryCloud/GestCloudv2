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
        public Dictionary<int, decimal> stock;
        public Dictionary<int, decimal> prices;
        public Dictionary<int, int> times;
        public string ProductName { get; set; }
        public ProductType productType;
        public Expansion expansion;
        public GestCloudDB db;

        //public int option;
        public Movement movement;
        private DataTable dt;
        public int OperationType;

        public ProductsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();

            ProductName = "";
            OperationType = 0;

            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio de Compra", typeof(decimal));
            dt.Columns.Add("Precio de Venta", typeof(decimal));
        }

        public ProductsView(int OperationType)
        {
            db = new GestCloudDB();
            dt = new DataTable();
            stock = new Dictionary<int, decimal>();
            prices = new Dictionary<int, decimal>();
            times = new Dictionary<int, int>();

            this.OperationType = OperationType;
            ProductName = "";

            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Stock", typeof(string));
            dt.Columns.Add("Precio de Venta", typeof(decimal));
        }

        public void SetExpansion(int num)
        {
            if (num > 0)
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
            return db.MTGCards.Where(c => c.ProductID == num).Include(c => c.expansion).First().expansion;
        }

        public List<ProductType> GetProductTypes()
        {
            return db.ProductTypes.OrderByDescending(ex => ex.Name).ToList();
        }

        public FrameworkDB.V1.Condition GetCondition(int num)
        {
            return db.Conditions.Where(ex => ex.ConditionID == num).First();
        }

        public FrameworkDB.V1.Condition GetConditionDefault()
        {
            return db.Conditions.Where(c => c.Code.Contains("NM")).First();
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
            movement.product = this.movement.product;
            movement.ProductID = this.movement.product.ProductID;
            movement.condition = this.movement.condition;
            movement.Quantity = Convert.ToDecimal(this.movement.Quantity);
            movement.IsAltered = Convert.ToInt16(this.movement.IsAltered);
            movement.IsFoil = Convert.ToInt16(this.movement.IsFoil);
            movement.IsPlayset = Convert.ToInt16(this.movement.IsPlayset);
            movement.IsSigned = Convert.ToInt16(this.movement.IsSigned);
            movement.PurchasePrice = Convert.ToDecimal(this.movement.PurchasePrice);
            movement.SalePrice = Convert.ToDecimal(this.movement.SalePrice);

            return movement;
        }

        public void UpdateTable()
        {
            if (productType != null && OperationType == 0)
            {
                switch (productType.Name)
                {
                    case "MTGCard":
                        if (expansion != null || ProductName.Length > 3)
                            products = db.Products.Where(pr => pr.Name.ToLower()
                                .Contains($"({expansion.Abbreviation.ToLower()})") &&
                                pr.Name.ToLower().Contains(ProductName.ToLower())).OrderBy(p => p.Name).ToList();

                        else
                            products = new List<Product>();
                        break;

                    default:
                        products = db.Products.Where(p => p.productType.ProductTypeID == productType.ProductTypeID
                            && p.Name.ToLower().Contains(ProductName.ToLower())).OrderBy(p => p.Name).ToList();
                        break;
                }
            }

            else if(OperationType > 0)
            {
                List<Movement> movements = db.Movements.Where(m => m.documentType.Name.Contains("Delivery") || 
                    m.documentType.Name.Contains("Invoice")).Include(m => m.documentType).Include(m => m.product).ToList();

                products = new List<Product>();

                foreach(Movement item in movements)
                {
                    if (!stock.ContainsKey(Convert.ToInt16(item.ProductID)))
                    {
                        stock.Add(Convert.ToInt16(item.ProductID), 0);
                        prices.Add(Convert.ToInt16(item.ProductID), 0);
                        times.Add(Convert.ToInt16(item.ProductID), 0);
                    }

                    prices[Convert.ToInt16(item.ProductID)] = prices[Convert.ToInt16(item.ProductID)] + Convert.ToDecimal(item.SalePrice);
                    times[Convert.ToInt16(item.ProductID)] = times[Convert.ToInt16(item.ProductID)] + 1;

                    if (item.documentType.Input == 1)
                        stock[Convert.ToInt16(item.ProductID)] = stock[Convert.ToInt16(item.ProductID)] + Convert.ToDecimal(item.Quantity);

                    else
                        stock[Convert.ToInt16(item.ProductID)] = stock[Convert.ToInt16(item.ProductID)] - Convert.ToDecimal(item.Quantity);

                    if(products.Where(p => p.ProductID == item.ProductID).Count() == 0)
                        products.Add(item.product);
                }
                products.OrderBy(p => p.Name);
            }

            else
                EV_FilterName(0);

            dt.Clear();
            if (OperationType == 0)
            {
                foreach (Product product in products)
                {
                    dt.Rows.Add(product.ProductID, product.Name, product.PurchasePrice1, product.SalePrice1);
                }
            }

            else
            {
                foreach (Product product in products)
                {
                    dt.Rows.Add(product.ProductID, product.Name, stock[product.ProductID].ToString("0.##"), prices[product.ProductID] / times[product.ProductID]);
                }
            }
        }

        public void EV_FilterName(int option)
        {
            switch(option)
            {
                case 0:
                    if (ProductName.Length > 3)
                        products = db.Products.Where(p => p.Name.ToLower().Contains(ProductName.ToLower())).OrderBy(p => p.Name).ToList();

                    else
                        products = new List<Product>();

                    break;

                case 1:
                        products = products.Where(p => p.Name.ToLower().Contains(ProductName.ToLower())).OrderBy(p => p.Name).ToList();
                    break;
            }
            
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
