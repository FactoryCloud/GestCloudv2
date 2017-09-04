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
using System.Windows.Shapes;
using FrameworkDB.V1;
using Microsoft.EntityFrameworkCore;


namespace GestCloudv2
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public sealed partial class NewUserWindow : Window
    {
        GestCloudDB db;
        public event EventHandler UpdateDataEvent;
        private int UpdateFlag;
        

        public NewUserWindow()
        {
            this.InitializeComponent();
            UpdateFlag = 0;
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            using (db = new GestCloudDB())
            {
                var newUser = new User()
                {
                    FirstName = firsnameText.Text,
                    LastName = lastnameText.Text,
                    Username = usernameText.Text,
                    Password = passwordText.Password
                };
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            MessageBoxResult result = MessageBox.Show("Datos guardados correctamente");
            firsnameText.Text = "";
            lastnameText.Text = "";
            usernameText.Text = "";
            passwordText.Password = "";

            UpdateData();
        }

        private void BacktoMenu(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void UpdateData()
        {
            UpdateFlag++;
            if (UpdateFlag >= 1)
            {
                if (this.UpdateDataEvent != null)
                {
                    this.UpdateDataEvent(this, EventArgs.Empty);
                }
            }
        }
    }
}