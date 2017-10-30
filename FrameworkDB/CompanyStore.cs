using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class CompanyStore
    {
        public int CompanyStoreID { get; set; }

        [ForeignKey("FK_CompaniesStores_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_CompaniesStores_StoreID_Stores")]
        public int? StoreID { get; set; }
        public virtual Store store { get; set; }
    }
}
