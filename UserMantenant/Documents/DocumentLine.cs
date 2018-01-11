using FrameworkDB.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class DocumentLine
    {
        // Common Values
        public string Code { get; set; }
        public string Name { get; set; }
        public Product product { get; set; }
        public decimal Quantity { get; set; }

        // Purchase Values
        public decimal PurchaseGrossPrice { get; set; }
        public decimal PurchaseGrossPriceFinal { get; set; }

        public decimal PurchaseDiscount1Percentage { get; set; }
        public decimal PurchaseDiscount1Amount { get; set; }

        public decimal PurchaseTaxBase { get; set; }
        public decimal PurchaseTaxBaseFinal { get; set; }

        public decimal PurchaseTaxPercentage { get; set; }
        public decimal PurchaseTaxAmount { get; set; }

        public decimal PurchaseEquSurPercentage { get; set; }
        public decimal PurchaseEquSurAmount { get; set; }

        public decimal PurchaseFinalPrice { get; set; }

        // Sale Values
        public decimal SaleGrossPrice { get; set; }
        public decimal SaleGrossPriceFinal { get; set; }

        public decimal SaleDiscount1Percentage { get; set; }
        public decimal SaleDiscount1Amount { get; set; }

        public decimal SaleTaxBase { get; set; }
        public decimal SaleTaxBaseFinal { get; set; }

        public decimal SaleTaxPercentage { get; set; }
        public decimal SaleTaxAmount { get; set; }

        public decimal SaleEquSurPercentage { get; set; }
        public decimal SaleEquSurAmount { get; set; }

        public decimal SaleFinalPrice { get; set; }
    }
}
