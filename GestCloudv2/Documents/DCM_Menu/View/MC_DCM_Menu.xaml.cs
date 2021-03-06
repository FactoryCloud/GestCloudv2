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

namespace GestCloudv2.Documents.DCM_Menu.View
{
    /// <summary>
    /// Interaction logic for MC_DCM_Menu.xaml
    /// </summary>
    public partial class MC_DCM_Menu : Page
    {
        public MC_DCM_Menu()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            DG_Items.MouseLeftButtonUp += new MouseButtonEventHandler(EV_FileSelected);
            DG_Items.MouseDoubleClick += new MouseButtonEventHandler(EV_FileOpen);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void EV_FileOpen(object sender, MouseButtonEventArgs e)
        {
            if (GetController().SelectedItem())
            {
                DG_Items.MouseLeftButtonUp -= EV_FileSelected;
                GetController().EV_CT_Load();
            }
        }

        private void EV_FileSelected(object sender, MouseButtonEventArgs e)
        {
            int num = DG_Items.SelectedIndex;
            if (num >= 0)
            {
                DataGridRow row = (DataGridRow)DG_Items.ItemContainerGenerator.ContainerFromIndex(num);
                DataRowView dr = row.Item as DataRowView;
                GetController().SetItem(Int32.Parse(dr.Row.ItemArray[0].ToString()));
            }
        }

        private void UpdateData()
        {
            DG_Items.ItemsSource = null;
            DG_Items.ItemsSource = GetController().itemsView.GetTable();
        }

        virtual public Controller.CT_DCM_Menu GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_DCM_Menu)a.MainFrame.Content;
        }
    }
}
