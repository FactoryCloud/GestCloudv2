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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GestCloudv2.Files.Nodes.PaymentMethods.PaymentMethodItem.PaymentMethodItem_Load.Controller
{
    /// <summary>
    /// Interaction logic for CT_STR_Item_Load.xaml
    /// </summary>
    public partial class CT_PMT_Item_Load : Main.Controller.CT_Common
    {
        public PaymentMethod paymentMethod;
        public List<Company> companies;
        public SubmenuItems submenuItems;

        public CT_PMT_Item_Load(PaymentMethod paymentMethod, int editable)
        {
            submenuItems = new SubmenuItems();
            companies = new List<Company>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 0);
            Information["entityValid"] = 1;

            foreach (CompanyPaymentMethod comppmt in db.CompaniesPaymentMethods.Where(c => c.PaymentMethodID== paymentMethod.PaymentMethodID).Include(c => c.company))
            {
                companies.Add(comppmt.company);
            }

            this.paymentMethod = paymentMethod;
        }

        public CT_PMT_Item_Load(PaymentMethod paymentMethod, int editable, int external):base(external)
        {
            submenuItems = new SubmenuItems();
            companies = new List<Company>();
            Information.Add("editable", editable);
            Information.Add("old_editable", 0);
            Information.Add("minimalInformation", 0);
            Information.Add("external", 1);
            Information["entityValid"] = 1;

            foreach (CompanyPaymentMethod comppmt in db.CompaniesPaymentMethods.Where(c => c.PaymentMethodID == paymentMethod.PaymentMethodID).Include(c => c.company))
            {
                companies.Add(comppmt.company);
            }

            this.paymentMethod = paymentMethod;
        }

        override public void EV_Start(object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public List<PaymentMethod> GetPaymentMethods()
        {
            return db.PaymentMethods.ToList();
        }

        public void SetPaymentMethodName(string name)
        {
            paymentMethod.Name = name;
            TestMinimalInformation();
        }

        public void SetPaymentMethodCode(int code)
        {
            paymentMethod.Code = code;
            TestMinimalInformation();
        }

        public void UpdateCompanies(int num)
        {
            if(companies.Contains(db.Companies.Where(s => s.CompanyID == num).First()))
            {
                companies.Remove(db.Companies.Where(c => c.CompanyID == num).Include(c => c.CompanyPaymentMethods).First());
            }

            else
            {
                companies.Add(db.Companies.Where(c => c.CompanyID == num).Include(c => c.CompanyPaymentMethods).First());
            }
            TestMinimalInformation();
        }

        public override void EV_ActivateSaveButton(bool verificated)
        {
            if(verificated)
            {
                Information["entityValid"] = 1;
            }

            else
            {
                Information["entityValid"] = 0;
            }

            TestMinimalInformation();
        }

        public void CleanName()
        {
            paymentMethod.Name = "";
            TestMinimalInformation();
        }

        public Boolean CompanyControlExist(string name)
        {
            List<Company> companies = db.Companies.ToList();
            foreach (var item in companies)
            {
                if ((item.Name.ToLower() == name.ToLower() && paymentMethod.Name.ToLower() != name.ToLower()) || name.Length == 0)
                {
                    CleanName();
                    return true;
                }
            }
            paymentMethod.Name = name;
            TestMinimalInformation();
            return false;
        }

        private void TestMinimalInformation()
        {
            if(paymentMethod.Name.Length > 0 && paymentMethod.Code > 0 && Information["entityValid"] == 1)
            {
                Information["minimalInformation"] = 1;
            }

            else
            {
                Information["minimalInformation"] = 0;
            }

            if (Information["editable"] == 0)
                TS_Page = new View.TS_PMT_Item_Load(Information["minimalInformation"], Information["external"]);
            else
                TS_Page = new View.TS_PMT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
            LeftSide.Content = TS_Page;
        }

        public void SaveNewPaymentMethod()
        {
            PaymentMethod paymentmethod1 = db.PaymentMethods.Where(c => c.PaymentMethodID == paymentMethod.PaymentMethodID).First();
            paymentmethod1.Code = paymentMethod.Code;
            paymentmethod1.Name = paymentMethod.Name;
            db.PaymentMethods.Update(paymentmethod1);

            List<CompanyPaymentMethod> companyPaymentMethods = db.CompaniesPaymentMethods.Where(c => c.PaymentMethodID == paymentMethod.PaymentMethodID).Include(c => c.company).ToList();
            foreach (CompanyPaymentMethod companyPaymentMethod in companyPaymentMethods)
            {
                if (!companies.Contains(companyPaymentMethod.company))
                {
                    db.CompaniesPaymentMethods.Remove(db.CompaniesPaymentMethods.Where(c => c.CompanyPaymentMethodID == companyPaymentMethod.CompanyPaymentMethodID).First());
                }
            }

            foreach (Company company in companies)
            {
                if (db.CompaniesPaymentMethods.Where(c => c.CompanyID == company.CompanyID && c.PaymentMethodID == paymentMethod.PaymentMethodID).ToList().Count == 0)
                {
                    db.CompaniesPaymentMethods.Add(new CompanyPaymentMethod
                    {
                        paymentMethod = paymentMethod,
                        company = company
                    });
                }
            }

            db.SaveChanges();
            MessageBox.Show("Datos guardados correctamente");

            Information["fieldEmpty"] = 0;
            CT_Menu();
        }

        public void MD_PaymentMethodsChange(int num)
        {
            if (num == 0)
                companies = new List<Company>();

            else
                companies = db.Companies.ToList();

            MC_Page = new View.MC_PMT_Item_Load_PaymentMethod_Companies(Information["external"]);
            MainContent.Content = MC_Page;
        }

        public void CT_Menu()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        override public void UpdateComponents()
        {
            switch (Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new View.NV_PMT_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PMT_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PMT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PMT_Item_Load_PaymentMethod(Information["external"]);
                    ChangeComponents();
                    break;

                case 2:
                    NV_Page = new View.NV_PMT_Item_Load(Information["external"]);
                    if (Information["editable"] == 0)
                        TS_Page = new View.TS_PMT_Item_Load(Information["minimalInformation"], Information["external"]);
                    else
                        TS_Page = new View.TS_PMT_Item_Load_Edit(Information["minimalInformation"], Information["external"]);
                    MC_Page = new View.MC_PMT_Item_Load_PaymentMethod_Companies(Information["external"]);
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    if (Information["fieldEmpty"] == 1)
                    {
                        MessageBoxResult result = MessageBox.Show("Usted ha realizado cambios, ¿Esta seguro que desea salir?", "Volver", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new PaymentMethodMenu.Controller.CT_PaymentMethodMenu();
                    break;

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }

        public void ControlFieldChangeButton(bool verificated)
        {
            TestMinimalInformation();
        }
    }
}