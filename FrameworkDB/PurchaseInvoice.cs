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
        public int PurchaseInvoiceID { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_PurchaseInvoices_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }
    }
}
