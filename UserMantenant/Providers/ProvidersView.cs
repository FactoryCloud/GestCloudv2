using Microsoft.EntityFrameworkCore;
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
    public class ProvidersView
    {
        private GestCloudDB db;
        private DataTable dt;
        public Provider ProviderSearch;
        public Provider SelectedProvider;

        public ProvidersView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            ProviderSearch = new Provider();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre Comercial", typeof(string));
            dt.Columns.Add("Subnombre", typeof(string));
            dt.Columns.Add("Número", typeof(string));
        }

        public Provider GetProvider (int num)
        {
            return db.Providers.Where(p => p.ProviderID == num).First();
        }

        public void UpdateTable()
        {
            List<Provider> providers = db.Providers.OrderBy(u => u.ProviderID).Include(c => c.entity).ToList();

            dt.Clear();
            foreach (var item in providers) 
            {
                dt.Rows.Add(item.ProviderID, item.entity.Name,item.entity.Subname,item.entity.Phone1);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
