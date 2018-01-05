using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        public int Code { get; set; }

        public int EntityID { get; set; }
        public virtual Entity entity { get; set; }

        public virtual List<SaleDelivery> SaleDeliveries { get; set; }
        public virtual List<SaleInvoice> SaleInvoices { get; set; }
        public virtual List<SaleOrder> SaleOrders { get; set; }

        public virtual List<ClientTax> clientTaxes { get; set; }
    }
}
