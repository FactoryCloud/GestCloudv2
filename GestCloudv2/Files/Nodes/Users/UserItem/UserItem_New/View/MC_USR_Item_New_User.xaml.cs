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

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_New.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_USR_Item_New_User : Page
    {
        public MC_USR_Item_New_User()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_UserType.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_UserType.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            TB_UserName.KeyUp += new KeyEventHandler(EV_UserName);
            TB_UserName.Loaded += new RoutedEventHandler(EV_UserName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_UserName.Text = GetController().user.Username;

            List<UserType> userTypes = GetController().GetUserTypes();
            foreach(var userType in userTypes)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{userType.Name}";
                temp.Name = $"userType{userType.UserTypeID}";
                CB_UserType.Items.Add(temp);
            }

            List<User> users = GetController().GetUsers();
            List<int> nums = new List<int>();
            foreach (var user in users)
            {
                nums.Add(user.Code);
            }

            for (int i = 1; i <= 20; i++)
            {
                if(!nums.Contains(i))
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{i}";
                    temp.Name = $"userCode{i}";
                    CB_UserCode.Items.Add(temp);
                }

            }
        }

        private void EV_UserName(object sender, RoutedEventArgs e)
        {
            if(TB_UserName.Text.Length == 0)
            {
                if (SP_Username.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }

                else if (SP_Username.Children.Count == 2)
                {
                    SP_Username.Children.RemoveAt(SP_Username.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede estar vacio";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }
                GetController().CleanUsername();
            }

            else if(TB_UserName.Text.Any(x => Char.IsWhiteSpace(x)))
            {
                if (SP_Username.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }

                else if (SP_Username.Children.Count == 2)
                {
                    SP_Username.Children.RemoveAt(SP_Username.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este campo no puede contener espacios";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }
                GetController().CleanUsername();
            }

            else if (GetController().UserControlExist(TB_UserName.Text))
            {
                if (SP_Username.Children.Count == 1)
                {
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este usuario ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }

                else if (SP_Username.Children.Count == 2)
                {
                    SP_Username.Children.RemoveAt(SP_Username.Children.Count - 1);
                    TextBlock message = new TextBlock();
                    message.TextWrapping = TextWrapping.WrapWithOverflow;
                    message.Text = "Este usuario ya existe";
                    message.HorizontalAlignment = HorizontalAlignment.Center;
                    SP_Username.Children.Add(message);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                if (SP_Username.Children.Count == 2)
                {
                    SP_Username.Children.RemoveAt(SP_Username.Children.Count - 1);
                }
                GetController().EV_UpdateIfNotEmpty(true);
            }
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_UserType.SelectedItem;
            GetController().SetUserType(Convert.ToInt32(temp1.Name.Replace("userType", "")));

            ComboBoxItem temp2 = (ComboBoxItem)CB_UserCode.SelectedItem;
            GetController().SetUserCode(Convert.ToInt32(temp2.Name.Replace("userCode", "")));
        }

        private Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Files.Nodes.Users.UserItem.UserItem_New.Controller.CT_USR_Item_New)a.MainFrame.Content;
        }
    }
}
