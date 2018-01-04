using FrameworkDB.V1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class ItemsView
    {
        protected GestCloudDB db;
        protected DataTable dt;

        public ItemsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
        }

        virtual public void UpdateTable()
        {
        }

        virtual public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
