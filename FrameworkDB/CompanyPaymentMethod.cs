using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class CompanyPaymentMethod
    {
        public int CompanyPaymentMethodID { get; set; }

        [ForeignKey("FK_CompaniesPaymentMethods_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company{ get; set; }

        [ForeignKey("FK_CompaniesPaymentMethods_PaymentMethodID_PaymentMethods")]
        public int? PaymentMethodID { get; set; }
        public virtual PaymentMethod paymentMethod { get; set; }
    }
}
