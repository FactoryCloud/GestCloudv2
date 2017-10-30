using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class PurchaseInvoice
    {
        int PurchaseInvoiceID { get; set; }

        [ForeignKey("CompanyID")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }
    }
}
