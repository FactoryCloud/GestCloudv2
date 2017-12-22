using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GestCloudv2.Files.Nodes.Companies.CompanyItem.CompanyItem_Load.View
{
    /// <summary>
    /// Interaction logic for MC_CPN_Item_New_Company.xaml
    /// </summary>
    public partial class MC_CPN_Item_Load_Company_Taxes : Page
    {
        public List<Tax> taxes;
        public List<Tax> equiSurs;
        public List<Tax> specTaxes;

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        public MC_CPN_Item_Load_Company_Taxes()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(EV_Start);

            CB_TaxPeriods.SelectionChanged += new SelectionChangedEventHandler(EV_CB_Changes);

            TB_Tax1.KeyUp += new KeyEventHandler(EV_Tax);
            TB_Tax2.KeyUp += new KeyEventHandler(EV_Tax);
            TB_Tax3.KeyUp += new KeyEventHandler(EV_Tax);
            TB_Tax4.KeyUp += new KeyEventHandler(EV_Tax);
            TB_Tax5.KeyUp += new KeyEventHandler(EV_Tax);

            TB_EquivalenceSurcharge1.KeyUp += new KeyEventHandler(EV_Tax);
            TB_EquivalenceSurcharge2.KeyUp += new KeyEventHandler(EV_Tax);
            TB_EquivalenceSurcharge3.KeyUp += new KeyEventHandler(EV_Tax);
            TB_EquivalenceSurcharge4.KeyUp += new KeyEventHandler(EV_Tax);
            TB_EquivalenceSurcharge5.KeyUp += new KeyEventHandler(EV_Tax);

            TB_SpecialTax1.KeyUp += new KeyEventHandler(EV_Tax);
            TB_SpecialTax2.KeyUp += new KeyEventHandler(EV_Tax);
            TB_SpecialTax3.KeyUp += new KeyEventHandler(EV_Tax);
            TB_SpecialTax4.KeyUp += new KeyEventHandler(EV_Tax);
            TB_SpecialTax5.KeyUp += new KeyEventHandler(EV_Tax);
        }

        private void EV_Start(object sender, RoutedEventArgs e)
        {
            List<TaxType> taxPeriods = GetController().GetTaxPeriods();
            foreach(TaxType item in taxPeriods)
            {
                ComboBoxItem temp = new ComboBoxItem();
                temp.Content = $"{item.StartDate.ToShortDateString()} - {item.EndDate.ToShortDateString()}";
                temp.Name = $"period{item.TaxTypeID}";
                CB_TaxPeriods.Items.Add(temp);
            }
            CB_TaxPeriods.SelectedIndex = 0;

            if(GetController().Information["editable"] == 0)
            {
                TB_Tax1.IsReadOnly = true;
                TB_Tax2.IsReadOnly = true;
                TB_Tax3.IsReadOnly = true;
                TB_Tax4.IsReadOnly = true;
                TB_Tax5.IsReadOnly = true;

                TB_EquivalenceSurcharge1.IsReadOnly = true;
                TB_EquivalenceSurcharge2.IsReadOnly = true;
                TB_EquivalenceSurcharge3.IsReadOnly = true;
                TB_EquivalenceSurcharge4.IsReadOnly = true;
                TB_EquivalenceSurcharge5.IsReadOnly = true;

                TB_SpecialTax1.IsReadOnly = true;
                TB_SpecialTax2.IsReadOnly = true;
                TB_SpecialTax3.IsReadOnly = true;
                TB_SpecialTax4.IsReadOnly = true;
                TB_SpecialTax5.IsReadOnly = true;
            }
        }

        private void EV_Tax(object sender, RoutedEventArgs e)
        {

            for (int i = 1; i <= 5; i++)
            {
                var TB_Tax = (TextBox)this.FindName($"TB_Tax{i}");

                if (Regex.Matches(TB_Tax.Text, "[a-zA-Z.]").Count > 0)
                {
                    TB_Tax.Text = Regex.Replace(TB_Tax.Text, "[.]", ",");
                    TB_Tax.Text = Regex.Replace(TB_Tax.Text, "[^0-9,]", "");
                    TB_Tax.SelectionStart = TB_Tax.Text.Length;
                }

                if (Regex.Matches(TB_Tax.Text, "[.,]").Count > 1)
                {
                    TB_Tax.Text = ReplaceLastOccurrence(TB_Tax.Text, ",", "");
                    TB_Tax.SelectionStart = TB_Tax.Text.Length;
                }


                if (string.IsNullOrEmpty(TB_Tax.Text))
                {
                    if (GetController().taxes.Where(t => t.Type == i).Count() > 0)
                    {
                        Tax tmp = GetController().taxes.Where(t => t.Type == i).First();
                        GetController().taxes.Remove(tmp);
                    }
                }

                else
                {
                    if (GetController().taxes.Where(t => t.Type == i).Count() > 0)
                        GetController().taxes.Where(t => t.Type == i).First().Percentage = Convert.ToDecimal(TB_Tax.Text.Replace(".", ",")) + 0.00M;

                    else
                    {
                        GetController().AddTaxes(
                        new Tax
                        {
                            Type = i,
                            Percentage = Convert.ToDecimal(TB_Tax.Text.Replace(".", ",")) + 0.00M
                        }, Convert.ToInt16(((ComboBoxItem)CB_TaxPeriods.SelectedItem).Name.Replace("period", "")));
                    }
                }

                var TB_RE = (TextBox)this.FindName($"TB_EquivalenceSurcharge{i}");

                if (Regex.Matches(TB_RE.Text, "[a-zA-Z.]").Count > 0)
                {
                    TB_RE.Text = Regex.Replace(TB_RE.Text, "[.]", ",");
                    TB_RE.Text = Regex.Replace(TB_RE.Text, "[^0-9,]", "");
                    TB_RE.SelectionStart = TB_RE.Text.Length;
                }

                if (Regex.Matches(TB_RE.Text, "[.,]").Count > 1)
                {
                    TB_RE.Text = ReplaceLastOccurrence(TB_RE.Text, ",", "");
                    TB_RE.SelectionStart = TB_RE.Text.Length;
                }

                if (string.IsNullOrEmpty(TB_RE.Text))
                {
                    if (GetController().equiSurs.Where(t => t.Type == i).Count() > 0)
                    {
                        Tax tmp = GetController().equiSurs.Where(t => t.Type == i).First();
                        GetController().equiSurs.Remove(tmp);
                    }
                }

                else
                {
                    if (GetController().equiSurs.Where(t => t.Type == i).Count() > 0)
                        GetController().equiSurs.Where(t => t.Type == i).First().Percentage = Convert.ToDecimal(TB_RE.Text.Replace(".", ",")) + 0.00M;

                    else
                    {
                        GetController().AddEquiSurs(
                        new Tax
                        {
                            Type = i,
                            Percentage = Convert.ToDecimal(TB_RE.Text.Replace(".", ",")) + 0.00M
                        }, Convert.ToInt16(((ComboBoxItem)CB_TaxPeriods.SelectedItem).Name.Replace("period", "")));
                    }
                }

                var TB_ST = (TextBox)this.FindName($"TB_SpecialTax{i}");

                if (Regex.Matches(TB_ST.Text, "[a-zA-Z.]").Count > 0)
                {
                    TB_ST.Text = Regex.Replace(TB_ST.Text, "[.]", ",");
                    TB_ST.Text = Regex.Replace(TB_ST.Text, "[^0-9,]", "");
                    TB_ST.SelectionStart = TB_ST.Text.Length;
                }

                if (Regex.Matches(TB_ST.Text, "[.,]").Count > 1)
                {
                    TB_ST.Text = ReplaceLastOccurrence(TB_ST.Text, ",", "");
                    TB_ST.SelectionStart = TB_ST.Text.Length;
                }

                if (string.IsNullOrEmpty(TB_ST.Text))
                {
                    if (GetController().specTaxes.Where(t => t.Type == i).Count() > 0)
                    {
                        Tax tmp = GetController().specTaxes.Where(t => t.Type == i).First();
                        GetController().specTaxes.Remove(tmp);
                    }
                }

                else
                {
                    if (GetController().specTaxes.Where(t => t.Type == i).Count() > 0)
                        GetController().specTaxes.Where(t => t.Type == i).First().Percentage = Convert.ToDecimal(TB_ST.Text.Replace(".", ",")) + 0.00M;

                    else
                    {
                        GetController().AddSpecTaxes(
                        new Tax
                        {
                            Type = i,
                            Percentage = Convert.ToDecimal(TB_ST.Text.Replace(".", ",")) + 0.00M
                        }, Convert.ToInt16(((ComboBoxItem)CB_TaxPeriods.SelectedItem).Name.Replace("period", "")));
                    }
                }
            }
        }

        private void EV_CB_Changes(object sender, RoutedEventArgs e)
        {
            ComboBoxItem temp2 = (ComboBoxItem)CB_TaxPeriods.SelectedItem;
            if (temp2 != null)
            {
                taxes = GetController().GetTaxes(Convert.ToInt32(temp2.Name.Replace("period", "")));
                equiSurs = GetController().GetEquiSurs(Convert.ToInt32(temp2.Name.Replace("period", "")));
                specTaxes = GetController().GetSpecTaxes(Convert.ToInt32(temp2.Name.Replace("period", "")));
                EV_UpdateTaxes();
            }
        }

        public void EV_UpdateTaxes()
        {
            for (int i = 1; i <= 5; i++)
            {
                // Tasas de IVA
                var TB_Tax = (TextBox)this.FindName($"TB_Tax{i}");

                if (taxes.Where(t => t.Type == i).Count() > 0)
                {
                    TB_Tax.Text = $"{taxes.Where(t => t.Type == i).First().Percentage}";
                    TB_Tax.IsReadOnly = true;
                }
                else
                    TB_Tax.Text = "";

                // Tasas de Equivalencia
                var TB_RE = (TextBox)this.FindName($"TB_EquivalenceSurcharge{i}");

                if (equiSurs.Where(t => t.Type == i).Count() > 0)
                {
                    TB_RE.Text = $"{equiSurs.Where(t => t.Type == i).First().Percentage}";
                    TB_RE.IsReadOnly = true;
                }
                else
                    TB_RE.Text = "";

                // Tasas especiales
                var TB_ST = (TextBox)this.FindName($"TB_SpecialTax{i}");

                if (specTaxes.Where(t => t.Type == i).Count() > 0)
                {
                    TB_ST.Text = $"{specTaxes.Where(t => t.Type == i).First().Percentage}";
                    TB_ST.IsReadOnly = true;
                }
                else
                    TB_ST.Text = "";
            }

            /*//Recargos de equivalencia
            if (equiSurs.Where(t => t.Type == 1).Count() > 0)
                TB_EquivalenceSurcharge1.Text = $"{equiSurs.Where(t => t.Type == 1).First().Percentage}";
            else
                TB_EquivalenceSurcharge1.Text = "";

            if (equiSurs.Where(t => t.Type == 2).Count() > 0)
                TB_EquivalenceSurcharge2.Text = $"{equiSurs.Where(t => t.Type == 2).First().Percentage}";
            else
                TB_EquivalenceSurcharge2.Text = "";

            if (equiSurs.Where(t => t.Type == 3).Count() > 0)
                TB_EquivalenceSurcharge3.Text = $"{equiSurs.Where(t => t.Type == 3).First().Percentage}";
            else
                TB_EquivalenceSurcharge3.Text = "";

            if (equiSurs.Where(t => t.Type == 4).Count() > 0)
                TB_EquivalenceSurcharge4.Text = $"{equiSurs.Where(t => t.Type == 4).First().Percentage}";
            else
                TB_EquivalenceSurcharge4.Text = "";

            if (equiSurs.Where(t => t.Type == 5).Count() > 0)
                TB_EquivalenceSurcharge5.Text = $"{equiSurs.Where(t => t.Type == 5).First().Percentage}";
            else
                TB_EquivalenceSurcharge5.Text = "";


            //Tasas Especiales
            if (specTaxes.Where(t => t.Type == 1).Count() > 0)
                TB_SpecialTax1.Text = $"{specTaxes.Where(t => t.Type == 1).First().Percentage}";
            else
                TB_SpecialTax1.Text = "";

            if (specTaxes.Where(t => t.Type == 2).Count() > 0)
                TB_SpecialTax2.Text = $"{specTaxes.Where(t => t.Type == 2).First().Percentage}";
            else
                TB_SpecialTax2.Text = "";

            if (specTaxes.Where(t => t.Type == 3).Count() > 0)
                TB_SpecialTax3.Text = $"{specTaxes.Where(t => t.Type == 3).First().Percentage}";
            else
                TB_SpecialTax3.Text = "";

            if (specTaxes.Where(t => t.Type == 4).Count() > 0)
                TB_SpecialTax4.Text = $"{specTaxes.Where(t => t.Type == 4).First().Percentage}";
            else
                TB_SpecialTax4.Text = "";

            if (specTaxes.Where(t => t.Type == 5).Count() > 0)
                TB_SpecialTax5.Text = $"{specTaxes.Where(t => t.Type == 5).First().Percentage}";
            else
                TB_SpecialTax5.Text = "";


            //Tasas de IVA
            if (taxes.Where(t => t.Type == 1).Count() > 0)
                TB_Tax1.Text = $"{taxes.Where(t => t.Type == 1).First().Percentage}";
            else
                TB_Tax1.Text = "";

            if (taxes.Where(t => t.Type == 2).Count() > 0)
                TB_Tax2.Text = $"{taxes.Where(t => t.Type == 2).First().Percentage}";
            else
                TB_Tax2.Text = "";

            if (taxes.Where(t => t.Type == 3).Count() > 0)
                TB_Tax3.Text = $"{taxes.Where(t => t.Type == 3).First().Percentage}";
            else
                TB_Tax3.Text = "";

            if (taxes.Where(t => t.Type == 4).Count() > 0)
                TB_Tax4.Text = $"{taxes.Where(t => t.Type == 4).First().Percentage}";
            else
                TB_Tax4.Text = "";

            if (taxes.Where(t => t.Type == 5).Count() > 0)
                TB_Tax5.Text = $"{taxes.Where(t => t.Type == 5).First().Percentage}";
            else
                TB_Tax5.Text = "";*/
        }

        private Controller.CT_CPN_Item_Load GetController()
        {
            Window mainWindow = Application.Current.MainWindow;
            var a = (Main.View.MainWindow)mainWindow;
            return (Controller.CT_CPN_Item_Load)a.MainFrame.Content;
        }
    }
}
