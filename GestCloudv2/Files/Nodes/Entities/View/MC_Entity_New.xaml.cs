using FrameworkDB.V1;
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

namespace GestCloudv2.Files.Nodes.Entities.View
{
    /// <summary>
    /// Interaction logic for MC_Entity_New.xaml
    /// </summary>
    public partial class MC_Entity_New : Page
    {
        public MC_Entity_New()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EV_Start);

            TB_Entity_Name.Text = GetController().entity.Name;
            TB_Entity_SubName.Text = GetController().entity.Subname;
            TB_Entity_Phone1.Text = GetController().entity.Phone1;
            TB_Entity_Phone2.Text = GetController().entity.Phone2;
            TB_Entity_Mobile.Text = GetController().entity.Mobile;
            TB_Entity_Contact.Text = GetController().entity.Contact;
            TB_Entity_NIF.Text = GetController().entity.NIF;
            TB_Entity_Address.Text = GetController().entity.Address;
            TB_Entity_Email.Text = GetController().entity.Email;
            CB_Entity_City.SelectionChanged += new SelectionChangedEventHandler(EV_CB_City);
            CB_Entity_Country.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Country);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            TB_Entity_Name.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_SubName.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Phone1.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Phone2.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Mobile.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Contact.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_NIF.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Address.KeyUp += new KeyEventHandler(EV_Keys);
            TB_Entity_Email.KeyUp += new KeyEventHandler(EV_Keys);

            /*List<City> cities = GetController().GetCities();
            foreach (City ct in cities)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{ct.Name}";
                temp.Name = $"City{ct.CityID}";
                CB_Entity_City.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_Entity_City.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("City", "")) == GetController().citySelected.CityID)
                {
                    CB_Entity_City.SelectedValue = item;
                    break;
                }
            }

            List<Country> countries = GetController().GetCountries();
            foreach (Country cty in countries)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{cty.Name}";
                temp.Name = $"Country{cty.CountryID}";
                CB_Entity_Country.Items.Add(temp);
            }

            foreach (ComboBoxItem item in CB_Entity_Country.Items)
            {
                if (Convert.ToInt16(item.Name.Replace("Country", "")) == GetController().countrySelected.CountryID)
                {
                    CB_Entity_Country.SelectedValue = item;
                    break;
                }
            }*/
        }

        public void EV_Keys(object sender, RoutedEventArgs e)
        {
            if (GetController().entity == null)
            {
                GetController().entity = new Entity
                {
                    Name = TB_Entity_Name.Text.ToString(),
                    Subname = TB_Entity_SubName.Text.ToString(),
                    Phone1 = TB_Entity_Phone1.Text.ToString(),
                    Phone2 = TB_Entity_Phone2.Text.ToString(),
                    Mobile = TB_Entity_Mobile.Text.ToString(),
                    Contact = TB_Entity_Contact.Text.ToString(),
                    NIF = TB_Entity_NIF.Text.ToString(),
                    Address = TB_Entity_Address.Text.ToString(),
                    Email = TB_Entity_Email.Text.ToString()
                };
            }

            else
            {
                GetController().entity.Name = TB_Entity_Name.Text.ToString();
                GetController().entity.Subname = TB_Entity_SubName.Text.ToString();
                GetController().entity.Phone1 = TB_Entity_Phone1.Text.ToString();
                GetController().entity.Phone2 = TB_Entity_Phone2.Text.ToString();
                GetController().entity.Mobile = TB_Entity_Mobile.Text.ToString();
                GetController().entity.Contact= TB_Entity_Contact.Text.ToString();
                GetController().entity.NIF = TB_Entity_NIF.Text.ToString();
                GetController().entity.Address = TB_Entity_Address.Text.ToString();
                GetController().entity.Email = TB_Entity_Email.Text.ToString();
            }

            if (TB_Entity_Name.Text.Length <= 30 && TB_Entity_SubName.Text.Length <= 30 && TB_Entity_Phone1.Text.Length <= 20 && TB_Entity_NIF.Text.Length <= 10 && TB_Entity_Name.Text.Length > 0 && TB_Entity_SubName.Text.Length > 0 && TB_Entity_Phone1.Text.Length > 0 && TB_Entity_NIF.Text.Length > 0)
            {
                GetController().EV_ActivateSaveButton(true);
            }
            else
            {
                GetController().EV_ActivateSaveButton(false);
            }

            if (!string.IsNullOrEmpty(TB_Entity_Name.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_SubName.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_Phone1.Text.ToString()) || !string.IsNullOrEmpty(TB_Entity_NIF.Text.ToString()))
            {
                GetController().EV_UpdateIfNotEmpty(true);
            }

            else
            {
                GetController().EV_UpdateIfNotEmpty(false);
            }
        }

        private void EV_CB_City(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp1 = (ComboBoxItem)CB_Entity_City.SelectedItem;
            if (temp1 != null)
            {
                //GetController().SetCitySelected(Convert.ToInt32(temp1.Name.Replace("City", "")));
            }
        }

        private void EV_CB_Country(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_Entity_Country.SelectedItem;
            if (temp2 != null)
            {
                //GetController().SetCountrySelected(Convert.ToInt32(temp2.Name.Replace("Country", "")));
            }
        }

        virtual public Main.Controller.CT_Common GetController()
        {
            return new Main.Controller.CT_Common();
        }
    }
}
