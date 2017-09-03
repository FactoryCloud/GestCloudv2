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
        UserView userView;
        public InfoUser_MainContent(User user, bool editable)
        {
            InitializeComponent();
            userView = new UserView(user);

            firsnameText.Text = userView.user.FirstName;
            lastnameText.Text = userView.user.LastName;
            usernameText.Text = userView.user.Username;

            if (editable)
                ChangeToEdit();
        }

        public void ChangeToEdit()
        {
            firsnameText.IsReadOnly = false;
            lastnameText.IsReadOnly = false;
            usernameText.IsReadOnly = false;
        }
    }
}
