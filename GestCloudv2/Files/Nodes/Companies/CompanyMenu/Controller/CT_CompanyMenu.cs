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

namespace GestCloudv2.Files.Nodes.Companies.CompanyMenu.Controller
{
    /// <summary>
    /// Interaction logic for CT_CompanyMenu.xaml
    /// </summary>
    public partial class CT_CompanyMenu : Main.Controller.CT_Common
    {
        public CompaniesView CompaniesView;
        public Company Company;

        public CT_CompanyMenu()
        {
            CompaniesView = new CompaniesView();
        }

        public void SetCompany(int num)
        {
            Company = db.Companies.Where(c => c.CompanyID == num).Include(c => c.CompaniesStores).First();
            //TS_Page = new WorkingBoard.View.TS_WB_ToDo();
            //LeftSide.Content = TS_Page;
        }

        override public void EV_Start (object sender, RoutedEventArgs e)
        {
            UpdateComponents();
        }

        public void MD_Change(int i)
        {
            Information["oldmode"] = Information["mode"];
            Information["mode"] = i;

            UpdateComponents();
        }

        public void CT_Main()
        {
            Information["controller"] = 0;
            ChangeController();
        }

        private void UpdateComponents()
        {
            switch(Information["mode"])
            {
                case 0:
                    ChangeComponents();
                    break;

                case 1:
                    NV_Page = new Files.Nodes.Companies.CompanyMenu.View.NV_CPN_Menu();
                    TS_Page = new Files.Nodes.Companies.CompanyMenu.View.TS_CPN_Menu(); ;
                    MC_Page = new Files.Nodes.Companies.CompanyMenu.View.MC_CPN_Menu(); ;
                    ChangeComponents();
                    break;

                case 2:
                    ChangeComponents();
                    break;

                case 3:
                    ChangeComponents();
                    break;

                case 4:
                    ChangeComponents();
                    break;
            }
        }

        private void ChangeController()
        {
            switch (Information["controller"])
            {
                case 0:
                    Main.View.MainWindow a = (Main.View.MainWindow)System.Windows.Application.Current.MainWindow;
                    a.MainFrame.Content = new Files.Controller.CT_Files();
                    break;

                case 1:
                    /*MainWindow b = (MainWindow)System.Windows.Application.Current.MainWindow;
                    b.MainFrame.Content = new Main.Controller.MainController();*/
                    break;
            }
        }
    }
}
