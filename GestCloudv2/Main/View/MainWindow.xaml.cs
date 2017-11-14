using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.EntityFrameworkCore;
using FrameworkDB.V1;

namespace GestCloudv2.Main.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GestCloudDB db;
        public Dictionary<string, string> Information;
        public User selectedUser;
        public Company selectedCompany;
        public List<UserPermission> UserPermissions;

        public MainWindow(User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            db = new GestCloudDB();

            UserPermissions = new List<UserPermission>();
            Information = new Dictionary<string, string>();
            this.selectedUser = user;

            UserPermissions = db.UserPermissions.Where(u => u.user == user)
                .Include(u => u.user).Include(u => u.userType).Include(u => u.permissionType).ToList();

            selectedCompany = db.Companies.First();

            MainFrame.Content = new Main.Controller.CT_Main();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro que desea salir?","Salir", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
