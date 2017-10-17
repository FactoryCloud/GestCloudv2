using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace FrameworkDB.V1
{
    public class RequestHelper
    {
        public XmlDocument makeRequest()
        {
            String method = "GET";
            String url = "https://www.mkmapi.eu/ws/v1.1/account";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            return doc;
            // proceed further
        }

        public XmlDocument gamesMakeRequest()
        {
            String method = "GET";
            String url = "https://www.mkmapi.eu/ws/v2.0/games";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            return doc;
            // proceed further
        }

        public void expansionsMakeRequest()
        {
            GestCloudDB db = new GestCloudDB();
            /*db.Database.ExecuteSqlCommand("TRUNCATE TABLE [MTGCards]");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Expansions]");
            db.SaveChanges();*/

            String method = "GET";
            String url = "https://www.mkmapi.eu/ws/v2.0/games/1/expansions";

            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            OAuthHeader header = new OAuthHeader();
            request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
            request.Method = method;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());

            XDocument xmlDoc = XDocument.Parse(doc.OuterXml);

            List<Expansion> List = xmlDoc.Descendants("expansion").Select(u => new Expansion
            {
                ExpansionID = Convert.ToInt32(u.Element("idExpansion").Value),
                EnName = u.Element("enName").Value,
                Abbreviation = u.Element("abbreviation").Value,
                Icon = Convert.ToInt32(u.Element("icon").Value),
                ReleaseDate = Convert.ToDateTime(u.Element("releaseDate").Value),
                IsReleased = Convert.ToInt32(Convert.ToBoolean(u.Element("isReleased").Value))
            }).ToList();

            List<Expansion> temp = db.Expansions.ToList();
            foreach(Expansion ex in List)
            {
                if(temp.Where(u=> u.ExpansionID == ex.ExpansionID).ToList().Count==0)
                {
                    db.Add(ex);
                }
                else
                {
                    Expansion nex = temp.First(u => u.ExpansionID == ex.ExpansionID);
                    if (ex.EnName != nex.EnName || ex.Abbreviation != nex.Abbreviation || 
                        ex.Icon != nex.Icon || ex.ReleaseDate != nex.ReleaseDate || ex.IsReleased != nex.IsReleased)
                    {
                        nex.EnName = ex.EnName;
                        nex.Abbreviation = ex.Abbreviation;
                        nex.Icon = ex.Icon;
                        nex.ReleaseDate = ex.ReleaseDate;
                        nex.IsReleased = ex.IsReleased;
                        db.Update(nex);
                    }
                }
            }

            foreach (Expansion ex in temp)
            {
                if (List.Where(u => u.ExpansionID == ex.ExpansionID).ToList().Count == 0)
                {
                    db.Remove(ex);
                }
            }

            db.SaveChanges();
        }

        public void singlesMakeRequest()
        {
            String method = "GET";

            GestCloudDB db = new GestCloudDB();
            /*db.Database.ExecuteSqlCommand("TRUNCATE TABLE [MTGCards]");
            db.SaveChanges();*/

            List<MTGCard> List = new List<MTGCard>();
            foreach (Expansion exp in db.Expansions.ToList())
            {
                String url = "https://www.mkmapi.eu/ws/v2.0/expansions/"+exp.ExpansionID.ToString()+"/singles";

                HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
                OAuthHeader header = new OAuthHeader();
                request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
                request.Method = method;

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XDocument xmlDoc = XDocument.Parse(doc.OuterXml);
                List<MTGCard> Temp = xmlDoc.Descendants("single").Select(u => new MTGCard
                {
                    ProductID = Convert.ToInt32(u.Element("idProduct").Value),
                    MetaproductID = Convert.ToInt32(u.Element("idMetaproduct").Value),
                    CountReprints = Convert.ToInt32(u.Element("countReprints").Value),
                    Number = u.Element("number").Value,
                    EnName = u.Element("enName").Value,
                    Rarity = u.Element("rarity").Value,
                    Website = u.Element("website").Value,
                    Image = u.Element("image").Value,
                    ExpansionID = exp.Id
                }).ToList();
                List.AddRange(Temp);
            }

            List<MTGCard> temp = db.MTGCards.ToList();
            foreach (MTGCard ex in List)
            {
                if (temp.Where(u => u.ProductID == ex.ProductID).ToList().Count == 0)
                {
                    db.Add(ex);
                }

                else
                {
                    MTGCard oldcard = temp.First(u => u.ProductID == ex.ProductID);
                    if (ex.MetaproductID != oldcard.MetaproductID || ex.CountReprints != oldcard.CountReprints ||
                        ex.EnName != oldcard.EnName || ex.Rarity != oldcard.Rarity || ex.Website != oldcard.Website ||
                        ex.Image != oldcard.Image || ex.ExpansionID != oldcard.ExpansionID || ex.Number != oldcard.Number)
                    {
                        oldcard.MetaproductID = ex.MetaproductID;
                        oldcard.CountReprints = ex.CountReprints;
                        oldcard.Number = ex.Number;
                        oldcard.EnName = ex.EnName;
                        oldcard.Rarity = ex.Rarity;
                        oldcard.Website = ex.Website;
                        oldcard.Image = ex.Image;
                        oldcard.ExpansionID = ex.ExpansionID;
                        db.Update(oldcard);
                    }
                }
            }

            foreach (MTGCard ex in temp)
            {
                if (List.Where(u => u.ProductID == ex.ProductID).ToList().Count == 0)
                {
                    db.Remove(ex);
                }
            }

            db.SaveChanges();
        }

        public void productsMakeRequest(List<Product> products)
        {
            GestCloudDB db = new GestCloudDB();

            String method = "GET";
            String urlbase = "https://www.mkmapi.eu/ws/v2.0/products/";

            foreach(var product in products)
            {
                String url = urlbase + $"{product.ExternalID}";
                HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
                OAuthHeader header = new OAuthHeader();
                request.Headers.Add(HttpRequestHeader.Authorization, header.getAuthorizationHeader(method, url));
                request.Method = method;

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XDocument xmlDoc = XDocument.Parse(doc.OuterXml);

                List<PriceGuide> prices = xmlDoc.Descendants("priceGuide").Select(u => new PriceGuide
                {
                    AVG = Convert.ToDecimal(u.Element("AVG").Value)/100,
                }).ToList();

                product.Price = prices.First().AVG;
                db.Products.Update(product);
            }

            db.SaveChanges();
        }
    }

     

    /// <summary>
    /// Class encapsulates tokens and secret to create OAuth signatures and return Authorization headers for web requests.
    /// </summary>
    class OAuthHeader
    {
        /// <summary>App Token</summary>
        protected String appToken = "COY9tatjFPDPPsuC";
        /// <summary>App Secret</summary>
        protected String appSecret = "8ZXmK9ZnNpqoriIjNs9eBcQgko2WeAdV";
        /// <summary>Access Token (Class should also implement an AccessToken property to set the value)</summary>
        protected String accessToken = "ieHjUg4ciRZDAtY4yXKB6GKJmZ2BxMiM";
        /// <summary>Access Token Secret (Class should also implement an AccessToken property to set the value)</summary>
        protected String accessSecret = "v09AwvnT2etBgDxkik45WIopaZrAjM6E";
        /// <summary>OAuth Signature Method</summary>
        protected String signatureMethod = "HMAC-SHA1";
        /// <summary>OAuth Version</summary>
        protected String version = "1.0";
        /// <summary>All Header params compiled into a Dictionary</summary>
        protected IDictionary<String, String> headerParams;

        /// <summary>
        /// Constructor
        /// </summary>
        public OAuthHeader()
        {
            // String nonce = Guid.NewGuid().ToString("n");
            String nonce = "53eb1f44909d6";
            // String timestamp = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
            String timestamp = "1407917892";
            /// Initialize all class members
            this.headerParams = new Dictionary<String, String>();
            this.headerParams.Add("oauth_consumer_key", this.appToken);
            this.headerParams.Add("oauth_token", this.accessToken);
            this.headerParams.Add("oauth_nonce", nonce);
            this.headerParams.Add("oauth_timestamp", timestamp);
            this.headerParams.Add("oauth_signature_method", this.signatureMethod);
            this.headerParams.Add("oauth_version", this.version);
        }

        /// <summary>
        /// Pass request method and URI parameters to get the Authorization header value
        /// </summary>
        /// <param name="method">Request Method</param>
        /// <param name="url">Request URI</param>
        /// <returns>Authorization header value</returns>
        public String getAuthorizationHeader(String method, String url)
        {
            /// Add the realm parameter to the header params
            this.headerParams.Add("realm", url);

            /// Start composing the base string from the method and request URI
            String baseString = method.ToUpper()
                              + "&"
                              + Uri.EscapeDataString(url)
                              + "&";

            /// Gather, encode, and sort the base string parameters
            SortedDictionary<String, String> encodedParams = new SortedDictionary<String, String>();
            foreach (KeyValuePair<String, String> parameter in this.headerParams)
            {
                if (false == parameter.Key.Equals("realm"))
                {
                    encodedParams.Add(Uri.EscapeDataString(parameter.Key), Uri.EscapeDataString(parameter.Value));
                }
            }

            /// Expand the base string by the encoded parameter=value pairs
            List<String> paramStrings = new List<String>();
            foreach (KeyValuePair<String, String> parameter in encodedParams)
            {
                paramStrings.Add(parameter.Key + "=" + parameter.Value);
            }
            String paramString = Uri.EscapeDataString(String.Join<String>("&", paramStrings));
            baseString += paramString;

            /// Create the OAuth signature
            String signatureKey = Uri.EscapeDataString(this.appSecret) + "&" + Uri.EscapeDataString(this.accessSecret);
            HMAC hasher = HMACSHA1.Create();
            hasher.Key = Encoding.UTF8.GetBytes(signatureKey);
            Byte[] rawSignature = hasher.ComputeHash(Encoding.UTF8.GetBytes(baseString));
            String oAuthSignature = Convert.ToBase64String(rawSignature);

            /// Include the OAuth signature parameter in the header parameters array
            this.headerParams.Add("oauth_signature", oAuthSignature);

            /// Construct the header string
            List<String> headerParamStrings = new List<String>();
            foreach (KeyValuePair<String, String> parameter in this.headerParams)
            {
                headerParamStrings.Add(parameter.Key + "=\"" + parameter.Value + "\"");
            }
            String authHeader = "OAuth " + String.Join<String>(", ", headerParamStrings);

            return authHeader;
        }
    }
}
