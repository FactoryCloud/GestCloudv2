using System;
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
using FrameworkDB.V1;

namespace GestCloudv2.Main.View
{
    /// <summary>
    /// Interaction logic for SC_Common.xaml
    /// </summary>
    public partial class SB_Common : Page
    {
        public SB_Common()
        {
            InitializeComponent();

            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).GR_Main.PreviewMouseDown += new MouseButtonEventHandler(EV_PopupHide);

            this.Loaded += new RoutedEventHandler(EV_Start);
        }

        public void EV_Start(object sender, RoutedEventArgs e)
        {
            InitializingCompany();
            InitializingUser();
        }

        private void EV_SetCompany(object sender, RoutedEventArgs e)
        {
            BT_Company.IsChecked = false;
            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).SetCompanySelected(Convert.ToInt16(((Button)sender).Tag));
            InitializingCompany();
        }

        private void EV_SetFiscalYear(object sender, RoutedEventArgs e)
        {
            BT_FiscalYear.IsChecked = false;
            ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).SetFiscalYearSelected(Convert.ToInt16(((Button)sender).Tag));
            InitializingFiscalYear();
        }

        public virtual void EV_SetUser(object sender, RoutedEventArgs e)
        {
        }

        private void EV_PopupHide(object sender, RoutedEventArgs e)
        {
            if (!BT_Company.IsMouseOver && !SP_Company.IsMouseOver)
            {
                if (PU_Company.IsOpen)
                    BT_Company.IsChecked = false;
            }

            if (!BT_User.IsMouseOver && !SP_User.IsMouseOver)
            {
                if (PU_User.IsOpen)
                    BT_User.IsChecked = false;
            }

            if (!BT_FiscalYear.IsMouseOver && !SP_FiscalYear.IsMouseOver)
            {
                if (PU_FiscalYear.IsOpen)
                    BT_FiscalYear.IsChecked = false;
            }
        }

        public void InitializingCompany()
        {
            LB_Company.Content = $"Empresa: {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.Code} - {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.Name}";

            SP_Company.Children.Clear();

            List<Company> companies = GetController().GetCompanies();
            foreach (Company c in companies)
            {
                if (c.CompanyID != ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID)
                {
                    Button button = new Button();
                    StackPanel panel = new StackPanel();
                    Label label = new Label();
                    label.Content = $"{c.Code} - {c.Name}";
                    panel.Children.Add(label);
                    button.Content = panel;
                    button.Width = BT_Company.ActualWidth;
                    button.Tag = c.CompanyID;
                    button.Click += new RoutedEventHandler(EV_SetCompany);

                    SP_Company.Children.Add(button);
                }
            }
            InitializingFiscalYear();
        }

        public void InitializingUser()
        {
            LB_User.Content = $"Usuario: {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedUser.Code} - {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedUser.entity.Name}, {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedUser.entity.Subname}";

            SP_User.Children.Clear();

            List<User> users = GetController().GetUsers();
            foreach (User u in users)
            {
                if (u.UserID != ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedUser.UserID)
                {
                    Button button = new Button();
                    StackPanel panel = new StackPanel();
                    Label label = new Label();
                    label.Content = $"{u.Code} - {u.Username}";
                    panel.Children.Add(label);
                    button.Content = panel;
                    button.Width = BT_User.ActualWidth;
                    button.Tag = u.UserID;
                    button.Click += new RoutedEventHandler(EV_SetUser);

                    SP_User.Children.Add(button);
                }
            }
        }

        public void InitializingFiscalYear()
        {
            
            LB_FiscalYear.Content = $"Periodo: {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.StartDate.ToString("dd/MM/yyyy")} - {((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.EndDate.ToString("dd/MM/yyyy")}";

            SP_FiscalYear.Children.Clear();

            List<FiscalYear> fiscalYears = GetController().GetFiscalYears();
            foreach (FiscalYear c in fiscalYears)
            {
                if ((c.CompanyID == ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedCompany.CompanyID) && (c.FiscalYearID != ((Main.View.MainWindow)System.Windows.Application.Current.MainWindow).selectedFiscalYear.FiscalYearID))
                {
                    Button button = new Button();
                    StackPanel panel = new StackPanel();
                    Label label = new Label();
                    label.Content = $"{c.StartDate.ToString("dd/MM/yyyy")} - {c.EndDate.ToString("dd/MM/yyyy")}";
                    panel.Children.Add(label);
                    button.Content = panel;
                    button.Width = BT_FiscalYear.ActualWidth;
                    button.Tag = c.FiscalYearID;
                    button.Click += new RoutedEventHandler(EV_SetFiscalYear);

                    SP_FiscalYear.Children.Add(button);
                }
            }
        }

        public virtual Controller.CT_Common GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Main.Controller.CT_Common)a.MainFrame.Content;
        }
    }
}
