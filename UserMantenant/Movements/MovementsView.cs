using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using System.Data;
using System.Collections;

namespace FrameworkView.V1
{
    public class MovementsView
    {
        public List<Movement> movements { get; set; }
        public GestCloudDB db;
        private DataTable dt;

        public MovementsView()
        {
            movements = new List<Movement>();

            dt = new DataTable();

            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Cantidad", typeof(float));
            dt.Columns.Add("Precio", typeof(decimal));
        }

        public void AddMovement(Movement mov)
        {
            movements.Add(mov);
        }

        public void UpdateTable()
        {
            dt.Clear();
            foreach (Movement item in movements)
            {
                dt.Rows.Add(item.ProductID, item.product.Name, item.Quantity, (item.Base*1+item.Tax));
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
