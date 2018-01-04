using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class ProviderTax
    {
        public int ProviderTaxID { get; set; }

        [ForeignKey("FK_ProvidersTaxes_ProviderID_Providers")]
        public int? ProviderID { get; set; }
        public virtual Provider provider { get; set; }

        [ForeignKey("FK_ProvidersTaxes_TaxID_Taxes")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }
    }
}
