using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FrameworkDB.V1
{
    public class Expansion
    {
        [XmlElement("idExanpasion")]
        public string ExpansionID { get; set; }

        [XmlElement("enName")]
        public string EnName { get; set; }

        [XmlElement("abbreviation")]
        public string Abbreviation { get; set; }

        [XmlElement("releaseDate")]
        public string ReleaseDate { get; set; }
    }

    [XmlRoot("Expansions")]
    public class ExpansionsManager
    {
        [XmlElement("Expansion", typeof(Expansion))]
        public List<Expansion> ExpansionsList { get; set; }

        public ExpansionsManager()
        {
            RequestHelper req = new RequestHelper();
            XDocument xmlDoc = XDocument.Parse(req.expansionsMakeRequest().OuterXml);
            ExpansionsList = xmlDoc.Descendants("expansion").Select(u => new Expansion
            {
                ExpansionID = u.Element("idExpansion").Value,
                EnName = u.Element("enName").Value,
                Abbreviation = u.Element("abbreviation").Value,
                ReleaseDate = u.Element("releaseDate").Value,
            }).ToList();
        }
    }
}
