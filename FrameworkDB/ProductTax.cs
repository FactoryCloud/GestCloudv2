using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class ProductTax
    {
        public int ProductTaxID { get; set; }

        [ForeignKey("FK_ProductsTaxes_ProductID_Products")]
        public int? ProductID { get; set; }
        public virtual Product product { get; set; }

        [ForeignKey("FK_ProductsTaxes_TaxID_Taxes")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }
    }
}
