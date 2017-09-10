using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FrameworkDB.V1;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Collections;

namespace FrameworkView.V1
{
    public class CardsView
    {
        private GestCloudDB db;
        private DataTable dt;
        public Expansion expansionSearch;
        public User SelectedUser;

        public CardsView()
        {
            db = new GestCloudDB();
            dt = new DataTable();
            expansionSearch = new Expansion();
            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("Producto", typeof(int));
            dt.Columns.Add("Expansion", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
        }

        public void SetExpansion(int num)
        {
            expansionSearch = db.Expansions.First(ex => ex.Id == num);
        }

        public List<Expansion> GetExpansions()
        {
            List<Expansion> expansions = db.Expansions.OrderBy(ex => ex.ExpansionID).ToList();
            return expansions;
        }

        public void UpdateTable()
        {
            List<MTGCard> cards = db.MTGCards.Include(u => u.expansion).OrderBy(u => u.ProductID).OrderBy(u => u.expansion.ExpansionID).ToList();

            dt.Clear();
            foreach (MTGCard item in cards)
            {
                dt.Rows.Add(item.Id, item.ProductID, item.expansion.EnName, item.EnName);
            }
        }

        public void UpdateFilteredTable()
        {
            List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.ProductID).OrderBy(u => u.expansion.ExpansionID).ToList();

            dt.Clear();
            foreach (MTGCard item in cards)
            {
                dt.Rows.Add(item.Id, item.ProductID, item.expansion.EnName, item.EnName);
            }
        }

        private Boolean CardFilterExpansion(MTGCard card)
        {
            return card.ExpansionID == expansionSearch.Id;
        }

        public IEnumerable GetTable()
        {
            UpdateTable();
            return dt.DefaultView;
        }

        public IEnumerable GetTableFiltered()
        {
            UpdateFilteredTable();
            return dt.DefaultView;
        }
    }
}
