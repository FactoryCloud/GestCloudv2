using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkView.V1
{
    public class UsersAccessControlView
    {
        private GestCloudDB db;
        private DataTable dt;
        private User user;
        public DateTime? dateStart { get; set;}
        public DateTime? dateEnd { get; set; }

        public UsersAccessControlView(User user)
        {
            db = new GestCloudDB();
            dt = new DataTable();
            this.user = user;
            dt.Columns.Add("Usuario", typeof(string));
            dt.Columns.Add("Fecha Entrada", typeof(string));
            dt.Columns.Add("Fecha Salida", typeof(string));
        }

        public IEnumerable GetTableAccess()
        {
            UpdateTableAccess();
            return dt.DefaultView;
        }

        public void UpdateTableAccess()
        {
            List<UserAccessControl> AccessControl;
            if (dateStart != null && dateEnd != null)
            {
                AccessControl = db.UsersAccessControl.Where(u => (u.user == user && dateEnd.Value.AddDays(1) > u.DateEndAccess && dateStart <= u.DateStartAccess))
                .Include(u => u.user).ToList();
            }
            else if (dateStart != null)
            {
                AccessControl = db.UsersAccessControl.Where(u => (u.user == user && dateStart <= u.DateStartAccess))
                 .Include(u => u.user).ToList();
            }
            else if (dateEnd != null)
            {
                AccessControl = db.UsersAccessControl.Where(u => (u.user == user && dateEnd.Value.AddDays(1) >= u.DateEndAccess))
                 .Include(u => u.user).ToList();
            }
            else
            {
                AccessControl = db.UsersAccessControl.Where(u => u.user == user)
                  .Include(u => u.user).ToList();
            }
            String format = "dd/MM/yyyy HH:mm:ss";
            dt.Clear();
            foreach (var item in AccessControl)
            {
                dt.Rows.Add(item.user.Username,  item.DateStartAccess.ToString(format) , item.DateEndAccess.ToString(format));
            }
        }
    }
}
