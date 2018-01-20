using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class ConfigurationUser
    {
        public int ConfigurationUserID { get; set; }

        [ForeignKey("FK_ConfigurationsUsers_ConfigurationID_Configurations")]
        public int? ConfigurationID { get; set; }
        public virtual Configuration configuration { get; set; }

        [ForeignKey("FK_ConfigurationsUsers_UserID_Users")]
        public int? UserID { get; set; }
        public virtual User user { get; set; }
    }
}
