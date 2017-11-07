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

        public int Cod { get; set; }

        public int EntityID { get; set; }
        public virtual Entity entity { get; set; }
    }
}
