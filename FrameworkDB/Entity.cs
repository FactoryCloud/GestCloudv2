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
        public string mobile { get; set; }*/
        public string NIF { get; set; }
        /*public string pc { get; set; }
        public string Email { get; set; }
        public string address { get; set; }
        public string contact { get; set; }*/

        [ForeignKey("FK_Entity_EntityTypeID_EntityTypes")]
        public int? EntityTypeID { get; set; }
        public virtual EntityType entityType { get; set; }

        [ForeignKey("FK_Entity_CountryID_Countries")]
        public int? CountryID { get; set; }
        public virtual Country country { get; set; }

        [ForeignKey("FK_Entity_CityID_Cities")]
        public int? CityID { get; set; }
        public virtual City city { get; set; }

        public virtual Client client { get; set; }

        public virtual User user { get; set; }

        public virtual List<Provider> providers { get; set; }
        public virtual List<Agents> agents { get; set; }
        //public virtual List<Employee> employeers { get; set; }
    }
}
