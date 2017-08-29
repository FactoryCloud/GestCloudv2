using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FrameworkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Collections;

namespace FrameworkView.V1
{
    public class UsersView 
    {
        private GestCloudDB db;
        private DataTable dt;

        public UsersView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido", typeof(string));
            dt.Columns.Add("Usuario", typeof(string));
        }

        public void UpdateTable()
        {
            List<User> users = db.Users.ToList();

            dt.Clear();
            foreach (var item in users)
            {
                dt.Rows.Add(item.UserID, item.FirstName, item.LastName, item.Username);
            }
        }
        
        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
