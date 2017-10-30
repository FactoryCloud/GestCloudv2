using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class City
    {
        [Key]
        public int id { get; set; }

        [StringLength(40)]
        public string name { get; set; }
        [StringLength(5)]
        public string pc { get; set; }

        public decimal country { get; set; }
    }
}
