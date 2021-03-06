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
using GestCloudv2;
using FrameworkDB.V1;

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_New.View
{
    /// <summary>
    /// Interaction logic for NV_USR_Item_New.xaml
    /// </summary>
    public partial class NV_USR_Item_New : Page
    {
        public NV_USR_Item_New()
        {
            InitializeComponent();
        }

        private void EV_MD_User(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(1,0);
        }

        private void EV_MD_Entity(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(2,0);
        }

        private void EV_MD_Configuration(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(5,0);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New)a.MainFrame.Content;
        }
    }
}
