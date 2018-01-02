using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public int? Code { get; set; }

        public decimal? SaleDiscount1 { get; set; }
        public decimal? SaleDiscount2 { get; set; }
        public decimal? SalePrice1 { get; set; }
        public decimal? SalePrice2 { get; set; }
        public decimal? PurchaseDiscount1 { get; set; }
        public decimal? PurchaseDiscount2 { get; set; }
        public decimal? PurchasePrice1 { get; set; }
        public decimal? PurchasePrice2 { get; set; }

        [ForeignKey("FK_Products_ProductTypeID_ProductTypes")]
        public int? ProductTypeID { get; set; }
        public virtual ProductType productType { get; set; }

        [ForeignKey("FK_Products_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        public int? ExternalID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public DateTime? DateLaunch { get; set; }

        public virtual List<Movement> Movements { get; set; }
        public virtual List<ProductTax> productsTaxes { get; set; }
    }
}
