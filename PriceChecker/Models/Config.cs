namespace PriceChecker.Models
{
    public class Config
    {
        public IEnumerable<string> recipients;
        public string networkUsername;
        public string networkPassword;
        public IEnumerable<ItemWebResource> webResources;

        public Config(IEnumerable<string> recipients, string networkUsername, string networkPassword, IEnumerable<ItemWebResource> webResources)
        {
            this.recipients = recipients;
            this.networkUsername = networkUsername;
            this.networkPassword = networkPassword;
            this.webResources = webResources;
        }
    }

}
