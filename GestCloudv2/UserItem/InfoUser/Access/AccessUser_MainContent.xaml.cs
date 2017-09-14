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
using FrameworkView.V1;

namespace GestCloudv2.UserItem.InfoUser
{
    /// <summary>
    /// Interaction logic for AccessUser_MainContent.xaml
    /// </summary>
    public partial class AccessUser_MainContent : Page
    {
        UsersAccessControlView usersControl;
        
        public AccessUser_MainContent()
        {
            InitializeComponent();
            usersControl = new UsersAccessControlView(GetController().userView.user);
            UpdateDataAccess();
        }

        public void UpdateDataAccess()
        {
            AccessUserTable.ItemsSource = null;
            AccessUserTable.ItemsSource = usersControl.GetTableAccess();
        }

        private void FilteredBetweenDate()
        {

        }

        private void AccessUserTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void DateStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
                usersControl.dateStart = null;
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
                usersControl.dateStart = date.Value;
            }
            UpdateDataAccess();
            //MessageBox.Show(date.Value.ToShortDateString());
        }

        public void DateExit_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                this.Title = "No date";
                usersControl.dateEnd = null;
            }
            else
            {
                // ... No need to display the time.
                this.Title = date.Value.ToShortDateString();
                usersControl.dateEnd = date.Value;
            }
            UpdateDataAccess();
            //MessageBox.Show(date.Value.ToShortDateString());
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
