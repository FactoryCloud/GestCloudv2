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
    public class ClientsView
    {
        private GestCloudDB db;
        private DataTable dt;
        public Client client;
        //public Client clientSearch;
        //public Client SelectedClient;
        public List<Client> clients;

        public ClientsView()
        {
            clients = new List<Client>();
            db = new GestCloudDB();
            dt = new DataTable();
            //clientSearch = new Client();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre Comerical", typeof(string));
            dt.Columns.Add("Subnombre", typeof(string));
            dt.Columns.Add("Número", typeof(string));
        }

        public void UpdateTable()
        {
            List<Client> clients = db.Clients.Include(c => c.entity).ToList();

            dt.Clear();
            foreach (var item in clients) 
            {
                dt.Rows.Add(item.ClientID, item.entity.Name,item.entity.Subname,item.entity.Phone1);
            }
        }

        public Client GetClient(int num)
        {
            return db.Clients.Where(ex => ex.ClientID == num).Include(pt => pt.entity ).First();
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
