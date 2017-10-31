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
            List<Client> clients = db.Clients.OrderByDescending(u => u.ClientID).ToList();

            dt.Clear();
            foreach (var item in clients) 
            {
                dt.Rows.Add(item.ClientID, item.EntityID);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
