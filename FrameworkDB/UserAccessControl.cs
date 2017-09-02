using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class UserAccessControl
    {
        public int UserAccessControlID { get; set; }

        public User UserID { get; set; }

        public AccessType AccessTypeID { get; set; }

        [Required]
        public DateTime DateStartAccess { get; set; }

        [Required]
        public DateTime DateEndAccess { get; set; }
    }
}
