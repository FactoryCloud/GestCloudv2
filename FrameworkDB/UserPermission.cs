using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class UserPermission
    {
        [Key]
        public int UserPermissionID { get; set; }
        
        [ForeignKey("UserID")]
        public int? UserID { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("UserTypeID")]
        public int? UserTypeID { get; set; }
        public virtual UserType userType { get; set; }

        [ForeignKey("PermissionTypeID")]
        public int? PermissionTypeID { get; set; }
        public virtual PermissionType permissionType { get; set; }
    }
}
