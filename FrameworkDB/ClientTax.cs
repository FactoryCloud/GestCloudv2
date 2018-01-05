using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class ClientTax
    {
        public int ClientTaxID { get; set; }

        [ForeignKey("FK_ClientsTaxes_ClientID_Clients")]
        public int? ClientID { get; set; }
        public virtual Client client { get; set; }

        [ForeignKey("FK_ClientsTaxes_TaxID_Taxess")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }

    }
}
