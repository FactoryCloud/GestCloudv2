using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class City
    {
        [Key]
        public int CityID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(5)]
        public string P_C { get; set; }

        [ForeignKey("FK_City_CountryID_Countries")]
        public int? CountryID { get; set; }
        public virtual Country countries { get; set; }
    }
}
