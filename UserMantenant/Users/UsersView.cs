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
        public User userSearch;
        public User SelectedUser;

        public UsersView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            userSearch = new User();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Usuario", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("NIF", typeof(string));
        }

        public void UpdateTable()
        {
            List<User> users = db.Users.OrderByDescending(u => u.Enabled).Include(u => u.entity).ToList();

            dt.Clear();
            foreach (var item in users)
            {
                if (item.entity != null)
                    dt.Rows.Add(item.UserID, item.Username, item.entity.Name, item.entity.NIF);

                else
                    dt.Rows.Add(item.UserID, item.Username, "", "");
            }
        }

        public void UpdateFilteredTable()
        {
            List<User> users = db.Users.Where(u=> UserFilterName(u)).OrderByDescending(u => u.Enabled).ToList();

            dt.Clear();
            foreach (var item in users)
            {
                if(item.entity != null)
                    dt.Rows.Add(item.UserID, item.Username, item.entity.Name, item.entity.NIF);

                else
                    dt.Rows.Add(item.UserID, item.Username, "", "");
            }
        }

        public void UpdateFilteredTableUserName()
        {
            List<User> users = db.Users.Where(u => UserFilterUserName(u)).OrderByDescending(u => u.Enabled).ToList();

            dt.Clear();
            foreach (var item in users)
            {
                if (item.entity != null)
                    dt.Rows.Add(item.UserID, item.Username, item.entity.Name, item.entity.NIF);

                else
                    dt.Rows.Add(item.UserID, item.Username, "", "");
            }
        }

        public void UpdateFilteredTableCod()
        {
            List<User> users = db.Users.Where(u => UserFilterCod(u)).OrderByDescending(u => u.Enabled).ToList();

            dt.Clear();
            foreach (var item in users)
            {
                if (item.entity != null)
                    dt.Rows.Add(item.UserID, item.Username, item.entity.Name, item.entity.NIF);

                else
                    dt.Rows.Add(item.UserID, item.Username, "", "");
            }
        }

        private Boolean UserFilterName(User user)
        {
            return user.entity.Name.ToLower().Contains(userSearch.entity.Name.ToLower()) || user.entity.Subname.ToLower().Contains(userSearch.entity.Subname.ToLower());
        }

        private Boolean UserFilterUserName(User user)
        {
            return user.Username.ToLower().Contains(userSearch.Username.ToLower());
        }

        private Boolean UserFilterCod(User user)
        {
            return user.UserID.ToString().Contains(userSearch.UserID.ToString());
        }

        public User UpdateUserSelected (int userID)
        {
            List<User> users = db.Users.Where(u => u.UserID == userID).ToList();
            SelectedUser = users[0];
            return SelectedUser;
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFiltered()
        {
            UpdateFilteredTable();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFilteredUserName()
        {
            UpdateFilteredTableUserName();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFilteredCod()
        {
            UpdateFilteredTableCod();
            return dt.DefaultView;
        }
    }
}
