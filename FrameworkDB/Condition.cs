using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Condition
    {
        public int ConditionID { get; set; }
        public string Name { get; set; }

        public virtual List<Movement> Movements { get; set; }
    }
}
