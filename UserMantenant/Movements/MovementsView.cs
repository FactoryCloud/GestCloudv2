using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using System.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace FrameworkView.V1
{
    public class MovementsView
    {
        public List<Movement> movements { get; set; }
        public GestCloudDB db;
        private Company company;
        private int option;
        private FiscalYear fiscalYear;
        private DateTime date;
        private DataTable dt;

        public MovementsView(Company company, int option)
        {
            db = new GestCloudDB();

            this.company = company;
            this.option = option;

            movements = new List<Movement>();

            dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Código", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Dto1", typeof(decimal));
            dt.Columns.Add("Dto2", typeof(decimal));
            dt.Columns.Add("Importe", typeof(decimal));
        }

        public decimal GetPurchaseGrossPrice()
        {
            decimal grossPrice = 0;
            foreach(Movement item in movements)
            {
                grossPrice = grossPrice +(Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity)); 
            }
            return grossPrice;
        }

        public decimal GetPurchaseDiscount()
        {
            decimal discount = 0;
            foreach (Movement item in movements)
            {
                discount = discount + (Convert.ToDecimal(item.product.PurchaseDiscount1) * Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return discount;
        }

        public decimal GetPurchaseTaxBase()
        {
            decimal taxBase = 0;
            foreach (Movement item in movements)
            {
                taxBase = taxBase + ((Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity))) - (Convert.ToDecimal(item.product.PurchaseDiscount1) * Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxBase;
        }

        public decimal GetPurchaseTaxAmount()
        {
            decimal taxAmount = 0;
            foreach (Movement item in movements)
            {
                decimal tax = 0;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= date
                    && tt.EndDate >= date && tt.Name.Contains("IVA")).First();
                if(db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).Count() > 0)
                    tax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax.Percentage;

                else
                    tax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax.Percentage;

                taxAmount = taxAmount + (tax * Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxAmount;
        }

        public decimal GetPurchaseFinalPrice()
        {
            decimal taxAmount = 0;
            foreach (Movement item in movements)
            {
                decimal tax = 0;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= date
                    && tt.EndDate >= date && tt.Name.Contains("IVA")).First();
                if (db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).Count() > 0)
                    tax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax.Percentage;

                else
                    tax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax.Percentage;

                taxAmount = taxAmount + (tax * Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity) / 100)
                    + ((Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity))) - (Convert.ToDecimal(item.product.PurchaseDiscount1) * Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxAmount;
        }

        public decimal GetSaleGrossPrice()
        {
            decimal grossPrice = 0;
            foreach (Movement item in movements)
            {
                grossPrice = grossPrice + (Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity));
            }
            return grossPrice;
        }

        public decimal GetSaleDiscount()
        {
            decimal discount = 0;
            foreach (Movement item in movements)
            {
                discount = discount + (Convert.ToDecimal(item.product.SaleDiscount1) * Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return discount;
        }

        public decimal GetSaleTaxBase()
        {
            decimal taxBase = 0;
            foreach (Movement item in movements)
            {
                taxBase = taxBase + ((Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity))) - (Convert.ToDecimal(item.product.SaleDiscount1) * Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxBase;
        }

        public decimal GetSaleTaxAmount()
        {
            decimal taxAmount = 0;
            foreach (Movement item in movements)
            {
                decimal tax = 0;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= date
                    && tt.EndDate >= date && tt.Name.Contains("IVA")).First();
                if (db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).Count() > 0)
                    tax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax.Percentage;

                else
                    tax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax.Percentage;

                taxAmount = taxAmount + (tax * Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxAmount;
        }

        public decimal GetSaleFinalPrice()
        {
            decimal taxAmount = 0;
            foreach (Movement item in movements)
            {
                decimal tax = 0;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= date
                    && tt.EndDate >= date && tt.Name.Contains("IVA")).First();
                if (db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).Count() > 0)
                    tax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax.Percentage;

                else
                    tax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax.Percentage;

                taxAmount = taxAmount + (tax * Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity) / 100)
                    + ((Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity))) - (Convert.ToDecimal(item.product.SaleDiscount1) * Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity) / 100);
            }
            return taxAmount;
        }

        public void SetDate(DateTime date)
        {
            this.date = date;
            fiscalYear = db.FiscalYears.Where(fy => fy.CompanyID == company.CompanyID && fy.StartDate <= date
            && fy.EndDate >= date).First();
        }

        public void MovementAdd(Movement mov)
        {
            movements.Add(mov);

            List<Movement> movementsDeleted = new List<Movement>();
            dt.Clear();
            foreach (Movement item in movements)
            {
                if (!movementsDeleted.Contains(item) && item.documentType != null)
                {
                    //if (item.documentType.Input == 0 && item.Quantity > 0)
                    if (item.documentType.Input == 0 && item.Quantity < 0) 
                        item.Quantity = Decimal.Multiply((decimal)item.Quantity, -1);

                    foreach (Movement item2 in movements)
                    {
                        if (item.product.ProductID == item2.product.ProductID && item.condition.ConditionID == item2.condition.ConditionID && item.IsSigned == item2.IsSigned &&
                            item.IsFoil == item2.IsFoil && item.store.StoreID == item2.store.StoreID && item.MovementID != item2.MovementID)
                        {
                            if (item2.documentType.Input == 0 && item2.Quantity > 0)
                                item2.Quantity = Decimal.Multiply((decimal)item2.Quantity, -1);

                            item.Quantity = item.Quantity + item2.Quantity;
                            movementsDeleted.Add(item2);
                        }
                    }
                }
            }

            foreach (Movement item in movementsDeleted)
            {
                movements.Remove(item);
            } 
        }

        public void MovementDelete (int num)
        {
            Movement movement = movements.Where(m => m.MovementID == num).First();
            movements.Remove(movement);
        }

        public int MovementNextID()
        {
            if (movements.Count > 0)
            {
                movements.OrderBy(m => m.MovementID);
                return movements.Last().MovementID + 1;
            }

            else
                return 1;
        }

        public int MovementNextID(int LastMovementID)
        {
            if (movements.Count > 0)
            {
                if (movements.OrderBy(m => m.MovementID).Last().MovementID > LastMovementID)
                    return movements.Last().MovementID + 1;

                else
                    return LastMovementID + 1;
            }

            else
                return LastMovementID + 1;
        }

        public void UpdateTable()
        {
            dt.Clear();
            foreach (Movement item in movements)
            {
                item.product = db.Products.Where(p => p.ProductID == item.ProductID).First();
                switch(option)
                {
                    case 1:
                        if (item.store != null)
                            dt.Rows.Add(item.MovementID, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                                ((decimal)item.PurchasePrice).ToString("0.00"), (Convert.ToDecimal(item.product.PurchaseDiscount1)).ToString("0.00"), (Convert.ToDecimal(item.product.PurchaseDiscount2)).ToString("0.00"), ((decimal)(item.PurchasePrice * item.Quantity)).ToString("0.00"));
                        else
                            dt.Rows.Add(item.MovementID, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                                ((decimal)item.PurchasePrice).ToString("0.00"), (Convert.ToDecimal(item.product.PurchaseDiscount1)).ToString("0.00"), (Convert.ToDecimal(item.product.PurchaseDiscount2)).ToString("0.00"), ((decimal)(item.PurchasePrice * item.Quantity)).ToString("0.00"));

                        break;

                    case 2:
                        if (item.store != null)
                            dt.Rows.Add(item.MovementID, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                                ((decimal)item.SalePrice).ToString("0.00"), (Convert.ToDecimal(item.product.SaleDiscount1)).ToString("0.00"), (Convert.ToDecimal(item.product.SaleDiscount2)).ToString("0.00"), ((decimal)(item.SalePrice * item.Quantity)).ToString("0.00"));
                        else
                            dt.Rows.Add(item.MovementID, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                                ((decimal)item.SalePrice).ToString("0.00"), (Convert.ToDecimal(item.product.SaleDiscount1)).ToString("0.00"), (Convert.ToDecimal(item.product.SaleDiscount2)).ToString("0.00"), ((decimal)(item.SalePrice * item.Quantity)).ToString("0.00"));
                        break;
                }
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
