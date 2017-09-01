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

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for InfoUser_Navigation.xaml
    /// </summary>
    public partial class InfoUser_Navigation : Page
    {
        public InfoUser_Navigation()
        {
            InitializeComponent();
        }

        private void BackUserListEvent(object sender, RoutedEventArgs e)
        {
            UserListLoad();
        }

        public void UserListLoad()
        {
            Window main = Application.Current.MainWindow;
            var a = (MainWindow)main;
            a.changeLeftSide(new UserList_ToolSide());
            a.changeMainContent(new UserList_MainContent());
            a.changeTopSide(new Main_Navigation());
        }
    }
}
