using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FrameworkDB.V1
{
    public class Expansion
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExpansionID { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(50)]
        public string EnName { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(50)]
        public string Abbreviation { get; set; }

        //[XmlElement("releaseDate")]
        //public string ReleaseDate { get; set; }

        public virtual List<MTGCard> MTGCards { get; set; }

        public string Print()
        {
            return ($"id={ExpansionID}, Name={EnName}, Abbreviation={Abbreviation}");
        }
    }
}
