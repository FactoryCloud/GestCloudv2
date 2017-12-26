using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class ProductTypeTax
    {
        public int ProductTypeTaxID { get; set; }

        [ForeignKey("FK_ProductTypesTaxes_ProductTypeID_ProductTypes")]
        public int? ProductTypeID { get; set; }
        public virtual ProductType productType { get; set; }

        [ForeignKey("FK_ProductTypesTaxes_TaxID_Taxes")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }

        public int Input { get; set; }
    }
}
