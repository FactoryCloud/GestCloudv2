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

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Tipo Producto", typeof(string));
            dt.Columns.Add("Condición", typeof(string));
            dt.Columns.Add("Firmado", typeof(string));
            dt.Columns.Add("Foil", typeof(string));
            dt.Columns.Add("Almacén", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
        }

        public void MovementAdd(Movement mov)
        {
            movements.Add(mov);

            List<Movement> movementsDeleted = new List<Movement>();
            dt.Clear();
            foreach (Movement item in movements)
            {
                if (!movementsDeleted.Contains(item))
                {
                    if (item.documentType.Input == 0 && item.Quantity > 0)
                        item.Quantity = Decimal.Multiply((decimal)item.Quantity, -1);

                    foreach (Movement item2 in movements)
                    {
                        if (item.product.ProductID == item2.product.ProductID && item.condition.ConditionID == item2.condition.ConditionID && item.IsSigned == item2.IsSigned &&
                            item.IsFoil == item2.IsFoil && item.store.StoreID == item2.store.StoreID && item.MovementID != item2.MovementID)
                        {
                            if (item2.documentType.Input == 0 && item2.Quantity > 0)
                                item2.Quantity = Decimal.Multiply((decimal)item2.Quantity, -1);

                            item.Quantity = item.Quantity + item2.Quantity;
                            movementsDeleted.Add(item2);
                        }
                    }
                }
            }

            foreach (Movement item in movementsDeleted)
            {
                movements.Remove(item);
            } 
        }

        public int MovementNextID()
        {
            if (movements.Count > 0)
            {
                movements.OrderBy(m => m.MovementID);
                return movements.Last().MovementID + 1;
            }

            else
                return 1;
        }

        public void UpdateTable()
        {
            dt.Clear();
            foreach (Movement item in movements)
            {
                dt.Rows.Add(item.MovementID, item.product.Name, item.product.productType.Name,
                    item.condition.Name, item.IsSigned, item.IsFoil, $"{item.store.Code}", item.Quantity);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
