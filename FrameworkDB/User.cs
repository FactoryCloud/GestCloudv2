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
        public int UserID { get; set; }

        public int UserCode { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string Mail { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [StringLength(10)]
        public string ActivationCode { get; set; }

        public virtual List<UserAccessControl> UsersAccessControl { get; set; }
        public virtual List<UserPermission> UserPermissions { get; set; }

        public User()
        {
            Mail = "test@test.com";
        }
    }
}
