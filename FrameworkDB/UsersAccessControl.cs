﻿using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDB.V1
{
    public class UsersAccessControl
    {
        private GestCloudDB db;
        private DataTable dt;

        public UsersAccessControl()
        {
            db = new GestCloudDB();
            dt = new DataTable();
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
            List<UserAccessControl> AccessControl = db.UsersAccessControl.Include(u => u.user).ToList();

            dt.Clear();
                foreach (var item in AccessControl)
                {
                    dt.Rows.Add(item.user.Username, item.DateStartAccess, item.DateEndAccess);
                }
        }
    }
}
