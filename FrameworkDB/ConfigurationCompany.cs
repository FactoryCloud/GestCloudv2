using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class ConfigurationCompany
    {
        public int ConfigurationCompanyID { get; set; }

        public int Value { get; set; }

        [ForeignKey("FK_ConfigurationsCompanies_ConfigurationID_Configurations")]
        public int? ConfigurationID { get; set; }
        public virtual Configuration configuration { get; set; }

        [ForeignKey("FK_ConfigurationsCompanies_CompanyID_Companies")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }
    }
}
