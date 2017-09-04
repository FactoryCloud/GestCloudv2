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

namespace GestCloudv2.UserItem
{
    /// <summary>
    /// Interaction logic for NewUser_ToolSide.xaml
    /// </summary>
    public partial class NewUser_ToolSide : Page
    {
        public NewUser_ToolSide()
        {
 
            InitializeComponent();
            //newUserWindows.firsnameText.KeyUp += new KeyEventHandler(Data_Control);
        }

        private void SaveUserEvent(object sender, RoutedEventArgs e)
        {
            Window main = Application.Current.MainWindow;
            var a = (MainWindow)main;
            var b = (Main.Main_Controller)a.MainPage.Content;
            var c = (NewUser_MainPage)b.MainContent.Content;
            c.SaveUser();
        }
    }
}
