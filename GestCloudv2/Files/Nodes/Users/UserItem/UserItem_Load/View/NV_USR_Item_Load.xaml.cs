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

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_Load.View
{
    /// <summary>
    /// Interaction logic for NV_USR_Item_Load.xaml
    /// </summary>
    public partial class NV_USR_Item_Load : Page
    {
        public NV_USR_Item_Load()
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

        private void EV_MD_Permissions(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(3,0);
        }

        private void EV_MD_Configuration(object sender, RoutedEventArgs e)
        {
            GetController().MD_Change(6, 0);
        }

        private void EV_CT_Menu(object sender, RoutedEventArgs e)
        {
            GetController().CT_Menu();
        }

        private Controller.CT_USR_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_USR_Item_Load)a.MainFrame.Content;
        }
    }
}
