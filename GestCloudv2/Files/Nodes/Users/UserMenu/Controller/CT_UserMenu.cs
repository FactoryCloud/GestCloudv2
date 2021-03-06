﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworkDB.V1;
using FrameworkView.V1;
using Microsoft.EntityFrameworkCore;

namespace GestCloudv2.Files.Nodes.Users.UserMenu.Controller
{
    /// <summary>
    /// Interaction logic for CT_StoreMenu.xaml
    /// </summary>
    public partial class CT_UserMenu : Main.Controller.CT_Common
    {
        public UsersView UsersView;
        public User user;

        public CT_UserMenu()
        {
            UsersView = new UsersView();
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void SetUser(int num)
        {
            user = db.Users.Where(c => c.UserID == num).Include(c => c.UserPermissions).Include(c => c.userType).Include(c=> c.entity).First();
            TS_Page = new Files.Nodes.Users.UserMenu.View.TS_USR_Menu();
            LeftSide.Content = TS_Page;
        }

        public void CT_UserNew()
        {
            Information["controller"] = 1;
            ChangeController();
        }

        public void CT_UserLoad()
        {
            Information["controller"] = 2;
            ChangeController();
        }

        public void CT_UserLoadEditable()
        {
            Information["controller"] = 3;
            ChangeController();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.Nodes.Users.UserMenu.View.NV_USR_Menu();
                    TS_Page = new Files.Nodes.Users.UserMenu.View.TS_USR_Menu(); ;
                    MC_Page = new Files.Nodes.Users.UserMenu.View.MC_USR_Menu(); ;
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 1:
                    Main.View.MainWindow b = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New();
                    break;

                case 2:
                    Main.View.MainWindow c = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    c.MainFrame.Content = new Files.Nodes.Users.UserItem.UserItem_Load.Controller.CT_USR_Item_Load(user, 0);
                    break;

                case 3:
                    Main.View.MainWindow d = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    d.MainFrame.Content = new Files.Nodes.Users.UserItem.UserItem_Load.Controller.CT_USR_Item_Load(user, 1);
                    break;
            }
        }
    }
}
