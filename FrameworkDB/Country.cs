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
        public int id { get; set; }

        [StringLength(40)]
        public string name { get; set; }
        [StringLength(2)]
        public string sig { get; set; }
        [StringLength(3)]
        public string iso { get; set; }
        [StringLength(1)]
        public string tip { get; set; }
    }
}
