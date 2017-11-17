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

namespace GestCloudv2.Files.Nodes.Stores.StoreItem.StoreItem_Load.View
{
    /// <summary>
    /// Interaction logic for TS_STR_Item_Load.xaml
    /// </summary>
    public partial class TS_STR_Item_Load : Page
    {
        public TS_STR_Item_Load(int num)
        {
            InitializeComponent();

            if(num >= 1)
            {
                //BT_StoreSave.IsEnabled = true;
            }
        }

        private void EV_CompanySave(object sender, RoutedEventArgs e)
        {
            GetController().SaveNewStore();
        }

        private Controller.CT_STR_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_STR_Item_Load)a.MainFrame.Content;
        }
    }
}