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
        User user;
        public InfoUser_MainContent(User user, bool editable)
        {
            InitializeComponent();
            this.user = user;

            this.Loaded += new RoutedEventHandler(StartInfoUser);

        }

        private void StartInfoUser(object sender, RoutedEventArgs e)
        {
            firsnameText.Text = user.FirstName;
            lastnameText.Text = user.LastName;
            usernameText.Text = user.Username;

            if(Convert.ToBoolean(GetController().Information["editable"]))
            {
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
