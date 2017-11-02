using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Entity
    {
        public int EntityID { get; set; }

        public int Cod { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public string Phone1 { get; set; }
        /*public string phone2 { get; set; }
        public string mobile { get; set; }
        public string nif { get; set; }
        public string pc { get; set; }*/
        public string Email { get; set; }
        /*public string address { get; set; }
        public string contact { get; set; }*/

        [ForeignKey("FK_Entity_CountryID_Countries")]
        public int? CountryID { get; set; }
        public virtual Country countries { get; set; }

        [ForeignKey("FK_Entity_CityID_Cities")]
        public int? CityID { get; set; }
        public virtual City cities { get; set; }
    }
}
