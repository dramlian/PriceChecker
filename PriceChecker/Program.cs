using PriceChecker.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        ConfigParser parser = new ConfigParser("InputConfig.xml");
        var config=parser.ParseTheValues();
        var priceChecker = new PriceComparer(new HttpHelper(new HttpClient()), config);
        var webResources = await priceChecker.DetermineAlert();
        MailFactory mailFactory = new MailFactory(webResources);
        MailSender mailSender = new MailSender(config);
        mailSender.SendMail("Price checker tool", mailFactory.CreateMail());
    }
}
