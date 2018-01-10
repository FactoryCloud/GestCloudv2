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
        public string Code { get; set; }
        public string Name { get; set; }
        public Product product { get; set; }
        public decimal Quantity { get; set; }

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
    }
}
