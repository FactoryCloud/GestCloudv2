﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkDB.V1
{
    public class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("FK_ProductTypes_TaxTypeID_TaxTypes")]
        public int? TaxID { get; set; }
        public virtual Tax tax { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
