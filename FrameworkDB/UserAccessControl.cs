using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Data;

namespace FrameworkDB.V1
{
    public class UserAccessControl
    {
        [Key]
        public int UserAccessControlID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("AccessTypeID")]
        public int AccessTypeID { get; set; }
        public virtual AccessType accessType {get; set;}

        [Required]
        public DateTime DateStartAccess { get; set; }

        public DateTime? DateEndAccess { get; set; }
    }
}
