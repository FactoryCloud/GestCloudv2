using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Configuration
    {
        public int ConfigurationID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultValue { get; set; }

        [ForeignKey("FK_Configurations_ConfigurationTypeID_ConfigurationTypes")]
        public int? ConfigurationTypeID { get; set; }
        public virtual ConfigurationType configurationType { get; set; }

        public virtual List<ConfigurationUser> ConfigurationsUsers { get; set; }

    }
}
