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
using System.Data;
using FrameworkView.V1;

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for InfoUser_MainContent.xaml
    /// </summary>
    public partial class InfoUser_MainContent : Page
    {
        public InfoUser_MainContent()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartInfoUser);
        }
        
        public void ControlBackUser()
        {
            MessageBox.Show($"primer valor{GetController().userView.user.FirstName} segundo{firsnameText.Text.ToString()}");
            if (firsnameText.Text == GetController().userView.user.FirstName || lastnameText.Text == GetController().userView.user.LastName || usernameText.Text == GetController().userView.user.Username)
            {
                GetController().Information["changes"] = 0;
                GetController().BackToMain();
            }
            else
            {
                GetController().Information["changes"]++;
                GetController().BackToMain();
            }
        }

        private void StartInfoUser(object sender, RoutedEventArgs e)
        {
            firsnameText.Text = GetController().userView.user.FirstName;
            lastnameText.Text = GetController().userView.user.LastName;
            usernameText.Text = GetController().userView.user.Username;

            if (GetController().Information["editable"] == 1)
            {
                //MessageBox.Show("modo editable");
                firsnameText.IsReadOnly = false;
                lastnameText.IsReadOnly = false;
                usernameText.IsReadOnly = false;
            }
        }

        private InfoUser.InfoUser_Controller GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (MainWindow)mainWindow;
            return (InfoUser.InfoUser_Controller)a.MainPage.Content;
        }
    }
}
