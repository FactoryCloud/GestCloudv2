using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class MovementTax
    {
        public int MovementTaxID { get; set; }

        [ForeignKey("FK_MovementTaxes_MovementID_Movements")]
        public int? MovementID { get; set; }
        public virtual Movement movement { get; set; }

        [ForeignKey("FK_MovementTaxes_TaxID_Taxes")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }
    }
}