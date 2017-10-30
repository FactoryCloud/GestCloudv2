using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [ForeignKey("FK_Products_ProductTypeID_ProductTypes")]
        public int? ProductTypeID { get; set; }
        public virtual ProductType productType { get; set; }

        [ForeignKey("CompanyID")]
        public int? CompanyID { get; set; }
        public virtual Company company { get; set; }

        public int? ExternalID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public DateTime? DateLaunch { get; set; }

        public virtual List<Movement> Movements { get; set; }
    }
}
