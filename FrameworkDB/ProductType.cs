using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class ProductType
    {
        public int ProductTypeID { get; set; }

        public int Code { get; set; }
        public int External { get; set; }

        public decimal? SaleDiscount1 { get; set; }
        public decimal? SaleDiscount2 { get; set; }
        public decimal? SalePrice1 { get; set; }
        public decimal? SalePrice2 { get; set; }
        public decimal? PurchaseDiscount1 { get; set; }
        public decimal? PurchaseDiscount2 { get; set; }
        public decimal? PurchasePrice1 { get; set; }
        public decimal? PurchasePrice2 { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
        public virtual List<ProductTypeTax> productTypesTaxes { get; set; }
    }
}
