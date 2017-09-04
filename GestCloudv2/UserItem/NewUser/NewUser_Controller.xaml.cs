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

namespace GestCloudv2.UserItem.NewUser
{
    /// <summary>
    /// Interaction logic for NewUser_Controller.xaml
    /// </summary>
    public partial class NewUser_Controller : Page
    {
        public Dictionary<string, int> Information;
        private Page MainContentUser;
        private Page ToolSideUser;
        private Page NavigationUser;

        public NewUser_Controller()
        {
            InitializeComponent();
            Information = new Dictionary<string, int>();
            Information.Add("mode", 0);

            this.Loaded += new RoutedEventHandler(StartNewUser_Event);
        }

        private void StartNewUser_Event(object sender, RoutedEventArgs e)
        {
            MainContentUser = new UserItem.NewUser_MainPage();
            ToolSideUser = new UserItem.NewUser_ToolSide();
            NavigationUser = new UserItem.NewUser_Navigation();
            ChangeEnviroment();
        }

        private void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    NavigationUser = new UserItem.NewUser_Navigation();
                    MainContentUser = new UserItem.NewUser_MainPage();
                    ToolSideUser = new UserItem.NewUser_ToolSide();
                    ChangeEnviroment();
                    break;

                /*case 1:
                    NavigationUser = new Main_Navigation();
                    MainContentUser = new UserList_MainContent();
                    ToolSideUser = new UserList_ToolSide();
                    ChangeEnviroment();
                    break;
                */
            }
        }

        private void ChangeEnviroment()
        {
            TopSide.Content = NavigationUser;
            MainContent.Content = MainContentUser;
            LeftSide.Content = ToolSideUser;
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            var a = (NewUser_ToolSide)LeftSide.Content;
            a.EnableButtonSaveUser(verificated);
        }
    }
}