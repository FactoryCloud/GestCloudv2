using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;

namespace FrameworkView.V1
{
    public class ProductView
    {
        public ProductType productType;
        public Expansion expansion;
        public Product product;
        public GestCloudDB db;

        public float Quantity { get; set; }
        public decimal Base { get; set; }
        public Condition condition;

        public ProductView()
        {
            db = new GestCloudDB();
        }
    }
}
