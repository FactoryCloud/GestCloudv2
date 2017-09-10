using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class MTGCard
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int ProductID { get; set; }
        public int MetaproductID { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(200)]
        public string EnName { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(300)]
        public string Website { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(300)]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        [StringLength(50)]
        public string Rarity { get; set; }

        [ForeignKey("ExpansionID")]
        public int? ExpansionID { get; set; }
        public virtual Expansion expansion { get; set; }
    }
}
