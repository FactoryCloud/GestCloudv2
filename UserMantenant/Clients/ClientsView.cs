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
    public class ClientsView
    {
        private GestCloudDB db;
        private DataTable dt;
        public Client clientSearch;
        public Client SelectedClient;

        public ClientsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            clientSearch = new Client();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre Comerical", typeof(string));
            dt.Columns.Add("Subnombre", typeof(string));
            dt.Columns.Add("Número", typeof(string));
        }

        public void UpdateTable()
        {
            List<Entity> entities = db.Entities.OrderByDescending(u => u.EntityID).ToList();

            dt.Clear();
            foreach (var item in entities) 
            {
                dt.Rows.Add(item.cod, item.name,item.subname,item.phone1);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
