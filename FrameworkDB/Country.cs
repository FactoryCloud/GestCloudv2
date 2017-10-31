using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(2)]
        public string Sig { get; set; }
        [StringLength(3)]
        public string Iso { get; set; }
        [StringLength(1)]
        public string Tip { get; set; }
    }
}
