using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class UserType
    {
        public int UserTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual List<UserPermission> UserPermissions { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
