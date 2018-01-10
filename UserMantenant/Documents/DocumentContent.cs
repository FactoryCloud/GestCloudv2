using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;

namespace FrameworkView.V1
{
    public class DocumentContent
    {
        public List<DocumentLine> Lines { get; set; }
        

        // Purchase Information
        public decimal PurchaseGrossPrice { get; set; }
        public decimal PurchaseGrossPriceFinal { get; set; }
        public decimal PurchaseTaxBase { get; set; }
        public decimal PurchaseTaxBaseFinal { get; set; }
        public decimal PurchaseDiscount { get; set; }
        public decimal PurchaseTaxAmount { get; set; }
        public decimal PurchaseEquSurAmount { get; set; }
        public decimal PurchaseFinalPrice { get; set; }

        public Dictionary<int, decimal> PurchaseTaxBases { get; set; }

        public Dictionary<int, decimal> PurchaseTaxAmounts { get; set; }

        public Dictionary<int, decimal> PurchaseEquSurAmounts { get; set; }

        public Dictionary<int, decimal> PurchaseFinalPrices { get; set; }

        // SaleInformation
        public decimal SaleGrossPrice { get; set; }
        public decimal SaleGrossPriceFinal { get; set; }
        public decimal SaleTaxBase { get; set; }
        public decimal SaleTaxBaseFinal { get; set; }
        public decimal SaleDiscount { get; set; }
        public decimal SaleTaxAmount { get; set; }
        public decimal SaleEquSurAmount { get; set; }
        public decimal SaleFinalPrice { get; set; }

        public Dictionary<int, decimal> SaleTaxBases { get; set; }

        public Dictionary<int, decimal> SaleTaxAmounts { get; set; }

        public Dictionary<int, decimal> SaleEquSurAmounts { get; set; }

        public Dictionary<int, decimal> SaleFinalPrices { get; set; }

        // private values
        private Company company { get; set; }
        private DateTime Date { get; set; }
        private GestCloudDB db { get; set; }
        private DataTable dt;
        private int Option { get; set; }

        public DocumentContent(int option, Company company, DateTime Date, List<Movement> Movements)
        {
            // purchases dictionaries
            PurchaseTaxBases = new Dictionary<int, decimal>();
            PurchaseTaxBases.Add(1, 0);
            PurchaseTaxBases.Add(2, 0);
            PurchaseTaxBases.Add(3, 0);
            PurchaseTaxBases.Add(4, 0);
            PurchaseTaxBases.Add(5, 0);

            PurchaseTaxAmounts = new Dictionary<int, decimal>();
            PurchaseTaxAmounts.Add(1, 0);
            PurchaseTaxAmounts.Add(2, 0);
            PurchaseTaxAmounts.Add(3, 0);
            PurchaseTaxAmounts.Add(4, 0);
            PurchaseTaxAmounts.Add(5, 0);

            PurchaseEquSurAmounts = new Dictionary<int, decimal>();
            PurchaseEquSurAmounts.Add(1, 0);
            PurchaseEquSurAmounts.Add(2, 0);
            PurchaseEquSurAmounts.Add(3, 0);
            PurchaseEquSurAmounts.Add(4, 0);
            PurchaseEquSurAmounts.Add(5, 0);

            PurchaseFinalPrices = new Dictionary<int, decimal>();
            PurchaseFinalPrices.Add(1, 0);
            PurchaseFinalPrices.Add(2, 0);
            PurchaseFinalPrices.Add(3, 0);
            PurchaseFinalPrices.Add(4, 0);
            PurchaseFinalPrices.Add(5, 0);

            // sale dictionaries
            SaleTaxBases = new Dictionary<int, decimal>();
            SaleTaxBases.Add(1, 0);
            SaleTaxBases.Add(2, 0);
            SaleTaxBases.Add(3, 0);
            SaleTaxBases.Add(4, 0);
            SaleTaxBases.Add(5, 0);

            SaleTaxAmounts = new Dictionary<int, decimal>();
            SaleTaxAmounts.Add(1, 0);
            SaleTaxAmounts.Add(2, 0);
            SaleTaxAmounts.Add(3, 0);
            SaleTaxAmounts.Add(4, 0);
            SaleTaxAmounts.Add(5, 0);

            SaleEquSurAmounts = new Dictionary<int, decimal>();
            SaleEquSurAmounts.Add(1, 0);
            SaleEquSurAmounts.Add(2, 0);
            SaleEquSurAmounts.Add(3, 0);
            SaleEquSurAmounts.Add(4, 0);
            SaleEquSurAmounts.Add(5, 0);

            SaleFinalPrices = new Dictionary<int, decimal>();
            SaleFinalPrices.Add(1, 0);
            SaleFinalPrices.Add(2, 0);
            SaleFinalPrices.Add(3, 0);
            SaleFinalPrices.Add(4, 0);
            SaleFinalPrices.Add(5, 0);

            dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Código", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Importe Bruto", typeof(decimal));
            dt.Columns.Add("Descuento", typeof(decimal));
            dt.Columns.Add("Importe Imponible", typeof(decimal));

            Lines = new List<DocumentLine>();

            this.company = company;
            this.Date = Date;
            this.Option = option;
            db = new GestCloudDB();

            if(option == 1)
                PurchaseUpdateComponents(Movements);

            if (option == 2)
                SaleUpdateComponents(Movements);
        }

        private void PurchaseUpdateComponents(List<Movement> Movements)
        {
            foreach (Movement item in Movements)
            {
                DocumentLine documentLine = new DocumentLine();

                documentLine.Code = $"{item.MovementID}";
                documentLine.Name = item.product.Name;
                documentLine.Quantity = Convert.ToDecimal(item.Quantity);
                documentLine.product = item.product;
                documentLine.PurchaseDiscount1Percentage = Convert.ToDecimal(item.Discount);
                //documentLine.PurchaseDiscount1Percentage = Convert.ToDecimal(item.product.PurchaseDiscount1); TU LINEA

                // Calculate Previous

                Tax purchaseTax;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= Date
                    && tt.EndDate >= Date && tt.Name.Contains("IVA")).First();

                if (db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).Count() > 0)
                    purchaseTax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax;

                else if (db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).Count() > 0)
                    purchaseTax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 1).Include(p => p.tax).First().tax;

                else
                    purchaseTax = new Tax { Percentage = 0 , Type = 1};

                documentLine.PurchaseTaxPercentage = purchaseTax.Percentage;

                // Calculate Gross Price
                decimal PurchaseGrossPriceTemp = Convert.ToDecimal(item.PurchasePrice);
                PurchaseGrossPrice = PurchaseGrossPrice + PurchaseGrossPriceTemp;

                documentLine.PurchaseGrossPrice = PurchaseGrossPriceTemp;

                decimal PurchaseGrossPriceFinalTemp = (Convert.ToDecimal(item.PurchasePrice) * Convert.ToDecimal(item.Quantity));

                PurchaseGrossPriceFinal = PurchaseGrossPriceFinal + PurchaseGrossPriceFinalTemp;

                documentLine.PurchaseGrossPriceFinal = PurchaseGrossPriceFinalTemp;

                // calculate Discount Amount
                decimal PurchaseDiscountTemp = (Convert.ToDecimal(item.Discount) * PurchaseGrossPriceTemp / 100);
                //decimal PurchaseDiscountTemp = (Convert.ToDecimal(item.product.PurchaseDiscount1) * PurchaseGrossPriceTemp / 100); TU LINEA
                PurchaseDiscount = PurchaseDiscount + PurchaseDiscountTemp;

                documentLine.PurchaseDiscount1Amount = PurchaseDiscountTemp;

                // Calculate Tax Base
                decimal PurchaseTaxBaseTemp = (PurchaseGrossPriceTemp - PurchaseDiscountTemp);
                PurchaseTaxBase = PurchaseTaxBase + PurchaseTaxBaseTemp;

                decimal PurchaseTaxBaseFinalTemp = (PurchaseGrossPriceTemp - PurchaseDiscountTemp) * Convert.ToDecimal(item.Quantity);
                PurchaseTaxBaseFinal = PurchaseTaxBaseFinal + PurchaseTaxBaseFinalTemp;
                PurchaseTaxBases[purchaseTax.Type] = PurchaseTaxBases[purchaseTax.Type] + PurchaseTaxBaseFinalTemp;

                documentLine.PurchaseTaxBase = PurchaseTaxBaseTemp;
                documentLine.PurchaseTaxBaseFinal = PurchaseTaxBaseFinalTemp;

                // Calculate IVA

                decimal purchaseTaxAmountTemp = purchaseTax.Percentage * PurchaseTaxBaseFinalTemp / 100;
                PurchaseTaxAmount = PurchaseTaxAmount + purchaseTaxAmountTemp;

                PurchaseTaxAmounts[purchaseTax.Type] = PurchaseTaxAmounts[purchaseTax.Type] + purchaseTaxAmountTemp;

                documentLine.PurchaseTaxAmount = purchaseTaxAmountTemp;

                // Calculate RE

                // Calculate Final Price
                decimal PurchaseFinalPriceTemp = PurchaseTaxBaseFinalTemp + purchaseTaxAmountTemp;
                PurchaseFinalPrice = PurchaseFinalPrice + PurchaseFinalPriceTemp;

                PurchaseFinalPrices[purchaseTax.Type] = PurchaseFinalPrices[purchaseTax.Type] + PurchaseFinalPriceTemp;

                documentLine.PurchaseFinalPrice = PurchaseFinalPriceTemp;

                Lines.Add(documentLine);
            }
        }

        private void SaleUpdateComponents(List<Movement> Movements)
        {
            foreach (Movement item in Movements)
            {
                // Calculate Previous

                Tax saleTax;
                TaxType taxType = db.TaxTypes.Where(tt => tt.CompanyID == company.CompanyID && tt.StartDate <= Date
                    && tt.EndDate >= Date && tt.Name.Contains("IVA")).First();

                if (db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).Count() > 0)
                    saleTax = db.ProductsTaxes.Where(pt => pt.ProductID == item.product.ProductID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax;

                else if (db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).Count() > 0)
                    saleTax = db.ProductTypesTaxes.Where(pt => pt.ProductTypeID == item.product.ProductTypeID && pt.tax.TaxTypeID == taxType.TaxTypeID && pt.Input == 0).Include(p => p.tax).First().tax;

                else
                    saleTax = new Tax { Percentage = 0, Type = 1 };

                // Calculate Gross Price
                decimal SaleGrossPriceTemp = (Convert.ToDecimal(item.SalePrice) * Convert.ToDecimal(item.Quantity));
                SaleGrossPrice = SaleGrossPrice + SaleGrossPriceTemp;

                // calculate Discount Amount
                decimal SaleDiscountTemp = Convert.ToDecimal(item.product.SaleDiscount1) * SaleGrossPriceTemp / 100;
                SaleDiscount = SaleDiscount + SaleDiscountTemp;

                // Calculate Tax Base
                decimal SaleTaxBaseTemp = SaleGrossPriceTemp - SaleDiscountTemp;
                SaleTaxBase = SaleTaxBase + SaleTaxBaseTemp;

                SaleTaxBases[saleTax.Type] = SaleTaxAmounts[saleTax.Type] + SaleTaxBaseTemp;

                // Calculate IVA
                decimal SaleTaxAmountTemp = saleTax.Percentage * SaleTaxBase / 100;
                SaleTaxAmount = SaleTaxAmount + SaleTaxAmountTemp;

                SaleTaxAmounts[saleTax.Type] = SaleTaxAmounts[saleTax.Type] + SaleTaxAmountTemp;

                // Calculate RE

                // Calculate Final Price
                decimal SaleFinalPriceTemp = SaleTaxBaseTemp + SaleTaxBaseTemp;
                SaleFinalPrice = SaleFinalPrice + SaleFinalPriceTemp;

                SaleFinalPrices[saleTax.Type] = SaleFinalPrices[saleTax.Type] + SaleFinalPriceTemp;
            }
        }

        public void SetDate(DateTime Date)
        {
            this.Date = Date;
        }

        public void UpdateTable()
        {
            dt.Clear();
            foreach (DocumentLine item in Lines)
            {
                //item.product = db.Products.Where(p => p.ProductID == item.ProductID).First();
                switch (Option)
                {
                    case 1:
                        dt.Rows.Add(item.Code, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                            ((decimal)item.PurchaseGrossPrice).ToString("0.00"), (Convert.ToDecimal(item.PurchaseDiscount1Percentage)).ToString("0.00"), ((decimal)(item.PurchaseTaxBaseFinal)).ToString("0.00"));

                        break;

                    case 2:
                        dt.Rows.Add(item.Code, item.product.Code, item.product.Name, ((decimal)item.Quantity).ToString("0.##"),
                            ((decimal)item.PurchaseGrossPrice).ToString("0.00"), (Convert.ToDecimal(item.PurchaseDiscount1Percentage)).ToString("0.00"), ((decimal)(item.PurchaseTaxBaseFinal)).ToString("0.00"));
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
