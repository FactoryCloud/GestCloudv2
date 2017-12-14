using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class TaxType
    {
        [Key]
        public int TaxTypeID { get; set; }

        public string Name { get; set; }

        [ForeignKey("FK_TaxTypes_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual List<Tax> taxes { get; set; }
    }
}
