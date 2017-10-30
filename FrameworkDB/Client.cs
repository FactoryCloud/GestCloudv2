using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [ForeignKey("FK_Clients_EntityID_Entities")]
        public int? EntityID { get; set; }
        public virtual Entity entity { get; set; }
    }
}
