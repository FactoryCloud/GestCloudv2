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

namespace GestCloudv2.Files.Nodes.Users.UserItem.UserItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_USR_Item_New_User.xaml
    /// </summary>
    public partial class MC_USR_Item_Load_User : Page
    {
        public MC_USR_Item_Load_User()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_UserCode.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            CB_UserType.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);
            TB_UserName.KeyUp += new KeyEventHandler(EV_UserName);
            TB_UserName.Loaded += new RoutedEventHandler(EV_UserName);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_UserName.Text = GetController().user.Username;

            if (GetController().Information["editable"] == 0)
            {
                TB_UserName.IsReadOnly = true;

                Thickness margin = new Thickness(20);

                TextBox TB_UserCode = new TextBox();
                TB_UserCode.Name = "TB_UserCode";
                TB_UserCode.Text = $"{GetController().user.Code}";
                TB_UserCode.VerticalAlignment = VerticalAlignment.Center;
                TB_UserCode.TextAlignment = TextAlignment.Center;
                TB_UserCode.Margin = margin;
                Grid.SetColumn(TB_UserCode, 2);
                Grid.SetRow(TB_UserCode, 2);

                TextBox TB_UserType = new TextBox();
                TB_UserType.Name = "TB_UserType";
                TB_UserType.Text = $"{GetController().user.userType.Name}";
                TB_UserType.VerticalAlignment = VerticalAlignment.Center;
                TB_UserType.TextAlignment = TextAlignment.Center;
                TB_UserType.Margin = margin;
                Grid.SetColumn(TB_UserType, 2);
                Grid.SetRow(TB_UserType, 3);

                GR_Main.Children.Add(TB_UserCode);
                GR_Main.Children.Add(TB_UserType);

                CB_UserCode.Visibility = Visibility.Hidden;
                CB_UserType.Visibility = Visibility.Hidden;
            }

            else
            {
                List<UserType> userTypes = GetController().GetUserTypes();
                foreach (var userType in userTypes)
                {
                    ComboBoxItem temp = new ComboBoxItem();
                    temp.Content = $"{userType.Name}";
                    temp.Name = $"userType{userType.UserTypeID}";
                    CB_UserType.Items.Add(temp);

                    if (GetController().user.userType != null)
                    {
                        if (String.Equals(GetController().user.userType.Name, $"{temp.Content}", StringComparison.CurrentCulture))
                            CB_UserType.SelectedItem = $"{temp.Content}";
                    }
                }

                if (GetController().user.userType != null)
                {
                    foreach (ComboBoxItem item in CB_UserType.Items)
                    {
                        if (item.Content.ToString() == $"{GetController().user.userType.Name}")
                        {
                            CB_UserType.SelectedValue = item;
                            break;
                        }
                    }
                }

                List<User> users = GetController().GetUsers();
                List<int> nums = new List<int>();
                foreach (var user in users)
                {
                    if(user.UserID != GetController().user.UserID)
                        nums.Add(Convert.ToInt16(user.Code));
                }

                for (int i = 1; i <= 20; i++)
                {
                    if (!nums.Contains(i))
                    {
                        ComboBoxItem temp = new ComboBoxItem();
                        temp.Content = $"{i}";
                        temp.Name = $"userCode{i}";
                        CB_UserCode.Items.Add(temp);
                    }
                }

                foreach (ComboBoxItem item in CB_UserCode.Items)
                {
                    if (item.Content.ToString() == $"{GetController().user.Code}")
                    {
                        CB_UserCode.SelectedValue = item;
                        break;
                    }
                }
            }
        }

        private void EV_UserName(object sender, RoutedEventArgs e)
        {
            if (GetController().Information["editable"] == 1)
            {
                if (TB_UserName.Text.Length == 0)
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

                else if (TB_UserName.Text.Any(x => Char.IsWhiteSpace(x)))
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
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_UserType.SelectedItem;
            if (temp1 != null)
            {
                GetController().SetUserType(Convert.ToInt32(temp1.Name.Replace("userType", "")));
            }

            ComboBoxItem temp2 = (ComboBoxItem)CB_UserCode.SelectedItem;
            if (temp2 != null)
            {
                GetController().SetUserCode(Convert.ToInt32(temp2.Name.Replace("userCode", "")));
            }
        }

        private Controller.CT_USR_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_USR_Item_Load)a.MainFrame.Content;
        }
    }
}
