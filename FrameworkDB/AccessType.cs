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

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
