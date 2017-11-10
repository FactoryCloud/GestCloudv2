using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Provider
    {
        
        public int ProviderID { get; set; }

        public int Cod { get; set; }

        public int EntityID { get; set; }
        public virtual Entity entity { get; set; }

        //public virtual List<Entity> entities { get; set; }
    }
}
