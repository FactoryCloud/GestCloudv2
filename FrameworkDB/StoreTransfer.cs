using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class StoreTransfer
    {
        public int StoreTransferID { get; set; }

        public string Code { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("FK_StoreTransfers_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        [ForeignKey("FK_StoreTransfers_StoreFromID_Stores")]
        public int? StoreFromID { get; set; }
        public virtual Store storeFrom { get; set; }

        [ForeignKey("FK_StoreTransfers_StoreToID_Stores")]
        public int? StoreToID { get; set; }
        public virtual Store storeTo { get; set; }
    }
}
