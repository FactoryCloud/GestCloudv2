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
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Número", typeof(string));
            dt.Columns.Add("Expansion", typeof(string));
            dt.Columns.Add("Rareza", typeof(string));
        }

        public void SetExpansion(int num)
        {
            expansionSearch = db.Expansions.First(ex => ex.Id == num);
        }

        public List<Expansion> GetExpansions()
        {
            List<Expansion> expansions = db.Expansions.OrderByDescending(ex => ex.ReleaseDate).ToList();
            return expansions;
        }

        public void UpdateTable()
        {
            List<MTGCard> cards = db.MTGCards.Include(u => u.expansion).OrderBy(u => u.Number).OrderBy(u => u.expansion.ReleaseDate).ToList();

            dt.Clear();
            foreach (MTGCard item in cards)
            {
                dt.Rows.Add(item.EnName, item.Number, item.expansion.Abbreviation, item.Rarity);
            }
        }

        public void UpdateFilteredTable()
        {
            List<MTGCard> cards = db.MTGCards.Where(u => CardFilterExpansion(u)).OrderBy(u => u.Rarity).ToList();

            dt.Clear();
            foreach (MTGCard item in cards)
            {
                dt.Rows.Add(item.EnName, item.Number, item.expansion.Abbreviation, item.Rarity);
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
