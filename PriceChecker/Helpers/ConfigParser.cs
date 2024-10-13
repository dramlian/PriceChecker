using System.Resources;
using System.Xml;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class ConfigParser
    {
        XmlDocument xmlDocument;

        public ConfigParser(string documentPath)
        {
            xmlDocument= new XmlDocument();
            xmlDocument.Load(documentPath);
        }

        public Config ParseTheValues()
        {
            XmlNodeList itemNodes = xmlDocument.GetElementsByTagName("ItemWebResource");
            List<ItemWebResource> resources = new ();

            foreach (XmlNode itemNode in itemNodes)
            {
                string url = itemNode["url"]?.InnerText ?? string.Empty;
                string regexPricePattern = itemNode["regexPricePattern"]?.InnerText ?? string.Empty;
                int priceGoal = int.Parse(itemNode["priceGoal"]?.InnerText ?? "0");
                resources.Add(new ItemWebResource(url,regexPricePattern,priceGoal,0));
            }

            return new Config(GetRecipientName(), GetNetworkUsername(), GetNetworkPassword(), resources);
        }

        private string GetNetworkUsername()
        {
            return GetElementInConfig("NetworkUserName");
        }

        private string GetNetworkPassword()
        {
            return GetElementInConfig("NetworkPassword");
        }

        private IEnumerable<string> GetRecipientName()
        {
            return GetElementInConfig("Recipient").Split(',');
        }

        private string GetElementInConfig(string elementName)
        {
            return xmlDocument.GetElementsByTagName(elementName).Item(0)?.InnerText ?? string.Empty;
        }
    }
}
