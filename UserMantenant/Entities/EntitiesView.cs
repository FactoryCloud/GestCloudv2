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
    public class EntitiesView
    {
        List<Entity> entities { get; set; }
        GestCloudDB db;
        int option;

        private DataTable dt;

        public EntitiesView(int opt)
        {
            db = new GestCloudDB();
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Subnombre", typeof(string));
            dt.Columns.Add("NIF", typeof(string));

            option = opt;
        }

        public List<EntityType> GetEntityTypes()
        {
            return db.EntityTypes.OrderBy(e=> e.Name).ToList();
        }

        public void UpdateTable()
        {
            switch(option)
            {
                case 0:
                    entities = db.Entities.OrderBy(e => e.Subname).OrderBy(e => e.Name).Include(e => e.entityType).ToList();
                    break;

                case 4:
                    List<Entity> userEntities = new List<Entity>();
                    List<User> users = db.Users.Where(u => u.EntityID != null).Include(u => u.entity).ToList();
                    foreach(User user in users)
                    {
                        userEntities.Add(user.entity);
                    }
                    entities = db.Entities.OrderBy(e => e.Subname).OrderBy(e => e.Name).Include(e => e.entityType).ToList();
                    entities = entities.Except(userEntities).ToList();
                    break;
            }

            dt.Clear();
            foreach (Entity ent in entities)
            {
                dt.Rows.Add(ent.EntityID, ent.Name, ent.Subname, ent.NIF);
            }
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }
    }
}
