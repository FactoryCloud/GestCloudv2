using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class EntityType
    {
        public int EntityTypeID { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public virtual List<Entity> entities { get; set; }
    }
}
