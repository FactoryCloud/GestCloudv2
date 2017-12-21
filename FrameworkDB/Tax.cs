using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Tax
    {
        [Key]
        public int TaxID { get; set; }

        [ForeignKey("FK_Taxes_TaxTypeID_TaxTypes")]
        public int? TaxTypeID { get; set; }
        public virtual TaxType taxType { get; set; }

        public int Type { get; set; }
        public decimal Percentage { get; set; }

        public virtual List<Movement> movements { get; set; }
        public virtual List<ProductType> productTypes { get; set; }
        public virtual List<Product> products { get; set; }
        public virtual List<ProductTypeTax> productTypeTaxes { get; set; }
        public virtual List<ProductTax> productsTaxes { get; set; }
        public virtual List<MovementTax> movementTaxes { get; set; }
    }
}
