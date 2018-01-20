using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class ConfigurationType
    {
        public int ConfigurationTypeID { get; set; }
        public string Name { get; set; }

        public virtual List<Configuration> Configurations { get; set; }
    }
}
