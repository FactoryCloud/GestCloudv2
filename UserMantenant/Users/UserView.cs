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

        public UserView(int userID)
        {
            db = new GestCloudDB();
            var users = db.Users.Where(c => c.UserID == userID).ToArray();
            user = new User
            {
                UserID = users[0].UserID,
                Username = users[0].Username,
                FirstName = users[0].FirstName,
                LastName = users[0].LastName,
                Password = users[0].Password
            };
        }
    }
}
