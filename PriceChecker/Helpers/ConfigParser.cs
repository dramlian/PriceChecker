using System.Resources;
using System.Xml;
using PriceChecker.Iterfaces;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class ConfigParser
    {
        XmlDocument _xmlDocument;
        ISecretGetter _secretGetter;

        public ConfigParser(string documentPath, ISecretGetter secretGetter)
        {
            if (!File.Exists(documentPath))
            {
                throw new FileNotFoundException($"No file at path {documentPath}");
            }
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(documentPath);
            _secretGetter = secretGetter;
        }

        public Config ParseTheValues()
        {
            XmlNodeList itemNodes = _xmlDocument.GetElementsByTagName("ItemWebResource");
            List<ItemWebResource> resources = new ();

            foreach (XmlNode itemNode in itemNodes)
            {
                string url = itemNode["url"]?.InnerText ?? string.Empty;
                string regexPricePattern = itemNode["regexPricePattern"]?.InnerText ?? string.Empty;
                double priceGoal = double.Parse(itemNode["priceGoal"]?.InnerText ?? "0");
                resources.Add(new ItemWebResource(url,regexPricePattern,priceGoal));
            }

            return new Config(GetRecipientName(), _secretGetter.GetPublicKey(), _secretGetter.GetSecretKey(), resources);
        }

        private IEnumerable<string> GetRecipientName()
        {
            return (_xmlDocument.GetElementsByTagName("Recipient").Item(0)?.InnerText ?? string.Empty).Split(',');
        }
    }
}
