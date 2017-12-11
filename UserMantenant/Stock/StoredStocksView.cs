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
    public class StoredStocksView
    {
        List<Movement> movements { get; set; }
        List<Movement> movementsOld;
        public string ProductName { get; set; }
        public GestCloudDB db;

        int option;
        public Movement movement;
        private List<Movement> original;
        public ProductType productType;
        public Expansion expansion;
        public Store store;
        public Product products;
        private DataTable dt;

        public StoredStocksView(int option, List<Movement> movements)
        {
            db = new GestCloudDB();
            store = new Store();
            productType = new ProductType();
            expansion = new Expansion();
            original = movements;
            movementsOld = new List<Movement>();
            movement = new Movement();
            this.movements = new List<Movement>();

            this.option = option;

            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Tipo Producto", typeof(string));
            dt.Columns.Add("Condición", typeof(string));
            dt.Columns.Add("Firmado", typeof(string));
            dt.Columns.Add("Foil", typeof(string));
            dt.Columns.Add("Almacén", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));

            switch(option)
            {
                case 1:
                    foreach (Movement mov in original)
                    {
                        if (mov.store != null)
                            movementsOld.Add(mov);
                    }
                    break;
            }
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

        public Product GetProduct(int num)
        {
            return db.Products.Where(p => p.ProductID == num).First();
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

        public Store GetStore(int num)
        {
            return db.Stores.Where(p => p.Code == num).First();
        }

        public Movement GetMovement(int num)
        {
            Movement temp = db.Movements.Where(m => m.MovementID == num).Include(m => m.product)
                .Include(m => m.condition).First();

            Movement mov = new Movement();
            mov.product = temp.product;
            mov.IsFoil = temp.IsFoil;
            mov.IsAltered = temp.IsAltered;
            mov.IsPlayset = temp.IsPlayset;
            mov.IsSigned = temp.IsSigned;
            mov.store = temp.store;
            mov.condition = temp.condition;

            return mov;
        }

        public Movement AddMovement(Movement movement)
        {
            movement.documentType = db.DocumentTypes.Where(d => d.Name == "StockAdjust").First();
            return movement;
        }

        public Movement UpdateMovement(Movement movement)
        {
            int input;
            if(movement.Quantity>0)
            {
                input = 1;
            }

            else
            {
                input = 0;
            }

            switch (option)
            {
                case 1:
                    movement.documentType = db.DocumentTypes.Where(d => d.Name == "StockAdjust" 
                    && d.Input == input).First();
                    break;
            }

            return movement;
        }

        public void UpdateTable()
        {
            List<DocumentType> validDocumenTypes = db.DocumentTypes.Where(d => !d.Name.Contains("Order")).ToList();

            foreach(DocumentType doc in validDocumenTypes)
            {
                movements.AddRange(db.Movements.Where(m => m.DocumentTypeID == doc.DocumentTypeID).Include(m => m.product).Include(m => m.product.productType).Include(m => m.condition).Include(m => m.store).Include(m => m.documentType).OrderBy(u => u.product.Name));
            }
            /*DocumentType documentTypeIn = db.DocumentTypes.Where(d => !d.Name.Contains("Order") && d.Input == 1).First();
            DocumentType documentTypeOut = db.DocumentTypes.Where(d => !d.Name.Contains("Order") && d.Input == 0).First();
            movements = db.Movements.Where(m => m.DocumentTypeID == documentTypeIn.DocumentTypeID || m.DocumentTypeID == documentTypeOut.DocumentTypeID).Include(m => m.product).Include(m => m.product.productType).Include(m => m.condition).Include(m => m.store).Include(m => m.documentType).OrderBy(u => u.product.Name).ToList();
            */
            foreach(Movement item in movementsOld)
            {
                movements.Add(item);
            }

            List<Movement> movementsDeleted = new List<Movement>();
            dt.Clear();
            foreach (Movement item in movements)
            {
                if (!movementsDeleted.Contains(item) && !movementsOld.Contains(item))
                {
                    if (item.documentType.Input == 0 && item.Quantity > 0)
                        item.Quantity = Decimal.Multiply((decimal)item.Quantity,-1);

                    foreach (Movement item2 in movements)
                    {
                        if (item.product.ProductID == item2.product.ProductID && item.condition.ConditionID == item2.condition.ConditionID && item.IsSigned == item2.IsSigned &&
                            item.IsFoil == item2.IsFoil && item.store.StoreID == item2.store.StoreID && (item.MovementID != item2.MovementID || movementsOld.Contains(item2)))
                        {
                            if (item2.documentType.Input == 0 && item2.Quantity > 0)
                                item2.Quantity = Decimal.Multiply((decimal)item2.Quantity, -1);

                            item.Quantity = item.Quantity + item2.Quantity;
                            movementsDeleted.Add(item2);
                        }
                    }
                }
            }

            foreach(Movement item in movementsDeleted)
            {
                movements.Remove(item);
            }

            foreach (Movement item in movements)
            {
                dt.Rows.Add(item.MovementID, item.product.Name, item.product.productType.Name,
                    item.condition.Name, item.IsSigned, item.IsFoil, $"{item.store.Code}", item.Quantity);
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
