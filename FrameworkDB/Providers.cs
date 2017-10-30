﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class Providers
    {
        [Key]
        public int id { get; set; }

        [StringLength(7)]
        public string cod { get; set; }
    }
}
