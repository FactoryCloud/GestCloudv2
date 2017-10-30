using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class SaleDelivery
    {
        public int SaleDeliveryID { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("FK_SaleDeliveries_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }
    }
}
