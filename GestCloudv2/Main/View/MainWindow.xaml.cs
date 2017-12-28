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
using System.Windows.Controls.Primitives;

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
        public List<Shortcuts.ShortcutDocument> shortcutDocuments;
        public Company selectedCompany;
        public FiscalYear selectedFiscalYear;
        public UserAccessControl uac;
        public List<UserPermission> userPermissions;

        public MainWindow(User user, UserAccessControl uac)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            Application.Current.MainWindow = this;
            db = new GestCloudDB();

            shortcutDocuments = new List<Shortcuts.ShortcutDocument>();
            userPermissions = new List<UserPermission>();
            Information = new Dictionary<string, string>();
            this.selectedUser = user;
            this.uac = uac;
            userPermissions = db.UserPermissions.Where(u => u.user == user)
                .Include(u => u.user).Include(u => u.userType).Include(u => u.permissionType).ToList();
            SetDefaultCompany();
            SetDefaultFiscalYear();

            MainFrame.Content = new Main.Controller.CT_Main();

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {             
        }

        public void SetCompanySelected(int num)
        {
            selectedCompany = db.Companies.Where(c => c.CompanyID == num).Include(c => c.fiscalYear).First();
            SetDefaultFiscalYear();
        }

        public void SetDefaultCompany()
        {
            selectedCompany = db.Companies.Where(c => c.CompanyID == selectedUser.CompanyID).Include(f => f.fiscalYear).First();
            SetDefaultFiscalYear();
        }

        public void SetFiscalYearSelected(int num)
        {
            selectedFiscalYear = db.FiscalYears.Where(c => c.FiscalYearID == num).First();
        }

        public void SetDefaultFiscalYear()
        {
            selectedFiscalYear = db.FiscalYears.Where(c => c.FiscalYearID == selectedCompany.FiscalYearID).First();
        }

        public void SetUserSelected(int num)
        {
            selectedUser = db.Users.Where(c => c.UserID == num).Include(u => u.entity).Include(u => u.UserPermissions).First();
            userPermissions = db.UserPermissions.Where(p => p.UserID == selectedUser.UserID).ToList();
        }

        public void SetUserAccessControl(UserAccessControl uac)
        {
            this.uac = uac;
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
                UserAccessControl uact = db.UsersAccessControl.Where(u => u.UserAccessControlID == uac.UserAccessControlID).First();
                uact.DateEndAccess = DateTime.Now;
                db.UsersAccessControl.Update(uact);
                db.SaveChanges();

                Application.Current.Shutdown();
            }
        }
    }
}
