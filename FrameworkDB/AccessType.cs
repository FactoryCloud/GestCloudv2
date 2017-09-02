using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class AccessType
    {
        public int AccessTypeID { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public virtual List<UserAccessControl> UsersAccessControl { get; set; }
    }
}
