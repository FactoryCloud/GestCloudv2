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
    public class UserView
    {
        GestCloudDB db;
        public User user;

        public UserView(User user)
        {
            this.user = user;
        }
    }
}
