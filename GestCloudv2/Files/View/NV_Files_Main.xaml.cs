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

namespace GestCloudv2.Files.View
{
    /// <summary>
    /// Interaction logic for NV_Files_Main.xaml
    /// </summary>
    public partial class NV_Files_Main : Page
    {
        public NV_Files_Main()
        {
            InitializeComponent();
        }

        private void EV_CT_Companies(object sender, RoutedEventArgs e)
        {
            GetController().CT_Companies();
        }

        private void EV_CT_Back(object sender, RoutedEventArgs e)
        {
            GetController().CT_Main();
        }

        private Files.Controller.CT_Files GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Controller.CT_Files)a.MainFrame.Content;
        }

        
    }
}
