using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows;

namespace FrameworkView.V1
{
    public class ConfigurationsView
    {
        List<Configuration> Configurations { get; set; }
        ConfigurationType configType { get; set; }
        GestCloudDB db;

        private DataTable dt;

        public ConfigurationsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Valor", typeof(string));
        }

        public void SetConfigType(int num)
        {
            configType = db.ConfigurationTypes.Where(c => c.ConfigurationTypeID == num).First();
        }

        public void UpdateTable()
        {
            if (configType == null)
                Configurations = db.Configurations.ToList();

            else
                Configurations = db.Configurations.Where(c => c.ConfigurationTypeID == configType.ConfigurationTypeID).ToList();

            dt.Clear();
            foreach(Configuration item in Configurations)
            {
                string value="";
                switch (item.DefaultValue)
                {
                    case 0:
                        value = "No";
                        break;

                    case 1:
                        value = "Si";
                        break;
                }
                dt.Rows.Add(item.ConfigurationID, item.Name, value);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
