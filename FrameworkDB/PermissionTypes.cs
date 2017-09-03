using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class PermissionType
    {
        public int PermissionTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Item { get; set; }

        [Required]
        [StringLength(50)]
        public string Subitem { get; set; }

        [Required]
        public int Mode { get; set; }


        public virtual List<UserPermission> UserPermissions { get; set; }
    }
}
