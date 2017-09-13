﻿using FrameworkDB.V1;
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

        public UsersAccessControlView(User user)
        {
            db = new GestCloudDB();
            dt = new DataTable();
            this.user = user;
            dt.Columns.Add("Usuario", typeof(string));
            dt.Columns.Add("Fecha Entrada", typeof(DateTime));
            dt.Columns.Add("Fecha Salida", typeof(DateTime));
        }

        public IEnumerable GetTableAccess()
        {
            UpdateTableAccess();
            return dt.DefaultView;
        }

        public void UpdateTableAccess()
        {
            //List<UserAccessControl> AccessControl = db.UsersAccessControl.Include(u => u.user).ToList();
             
            List<UserAccessControl> AccessControl = db.UsersAccessControl.Where(u => u.user == user)
                .Include(u => u.user).ToList();
            //TimeZoneInfo dataStartAcess = TimeZoneInfo.FindSystemTimeZoneById("Central European Standad Time");
            var usCulture = "en-US";
            dt.Clear();
            foreach (var item in AccessControl)
            {
                DateTime date;
                if(DateTime.TryParse(item.DateStartAccess.ToString(), out date) && DateTime.TryParse(item.DateEndAccess.ToString(), out date))
                {
                    dt.Rows.Add(item.user.Username, DateTime.Parse(item.DateStartAccess.ToString(), new CultureInfo(usCulture, false)), DateTime.Parse(item.DateEndAccess.ToString(), new CultureInfo(usCulture, false)));
                }
            }
        }
    }
}
