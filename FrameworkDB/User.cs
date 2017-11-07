using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public int Code { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(10)]
        public string ActivationCode { get; set; }

        [Required]
        public int Enabled { get; set; }

        [ForeignKey("FK_Users_UserTypeID_UserTypes")]
        public int? UserTypeID { get; set; }
        public virtual UserType userType { get; set; }

        [ForeignKey("FK_Users_EntityID_Entity")]
        public int? EntityID { get; set; }
        public virtual Entity entity { get; set; }

        public virtual List<UserAccessControl> UsersAccessControl { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }
    }
}
