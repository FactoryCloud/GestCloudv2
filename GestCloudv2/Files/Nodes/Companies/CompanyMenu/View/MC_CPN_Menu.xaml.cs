﻿using System;
using System.Collections.Generic;
using System.Data;
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

namespace GestCloudv2.Files.Nodes.Companies.CompanyMenu.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Menu.xaml
    /// </summary>
    public partial class MC_CPN_Menu : Page
    {
        public MC_CPN_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_Companies.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_Companies.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().company != null)
            {
                DG_Companies.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_CompanyLoad();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Companies.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Companies.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetCompany(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Companies.ItemsSource = null;
            DG_Companies.ItemsSource = GetController().CompaniesView.GetTable();
        }

        private Controller.CT_CompanyMenu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CompanyMenu)a.MainFrame.Content;
        }
    }
}
