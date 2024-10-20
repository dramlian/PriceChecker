using PriceChecker.Helpers;
using PriceChecker.Iterfaces;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            IAppLogger messageLogger = new ConsoleColorHelper();
            ISecretGetter secretGeter = new EnvironmentSecretGetter(messageLogger);
            var httpHelper = new HttpHelper(new HttpClient(), messageLogger);

            var config = new ConfigParser("InputConfig.xml", secretGeter).ParseTheValues();
            var priceChecker = new PriceComparer(httpHelper, config.webResources, messageLogger);
            var webResources = await priceChecker.FetchAndComparePrices();

            IMailFactory mailFactory = new SimpleMailFactory(webResources);
            MailSender mailSender = new MailSender(config, messageLogger);
            mailSender.SendMail("Price checker tool", mailFactory.CreateMail());
            await Task.Delay(TimeSpan.FromMinutes(30));
        }

    }
}
