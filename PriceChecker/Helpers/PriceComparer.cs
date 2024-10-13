using System.Text.RegularExpressions;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class PriceComparer
    {
        HttpHelper _httpHelper;
        Config _config;
        ConsoleColorHelper _consoleHelper;

        public PriceComparer(HttpHelper httpHelper, Config config)
        {
            _httpHelper= httpHelper;
            _config= config;
            _consoleHelper=new ConsoleColorHelper();
        }

        public async Task<IEnumerable<ItemWebResource>> DetermineAlert()
        {
            var urls=_config.webResources.Select(x=>x.url) ?? Enumerable.Empty<string>();
            var urlsToWebValue = (await _httpHelper.GetMultipleRequests(urls)).ToDictionary();
            return ParsePriceAndDetermineAlert(urlsToWebValue);
        }

        private IEnumerable<ItemWebResource> ParsePriceAndDetermineAlert(Dictionary<string, string> urlsToWebValue)
        {
            List<ItemWebResource> returnVal = new (); 
            foreach(var urlToWebValue in urlsToWebValue)
            {
                var itemWebResource = _config.webResources.Where(x => x.url.Equals(urlToWebValue.Key)).First();
                if(itemWebResource is not null)
                {
                    if(IsValueSmallerOrEqualThanTargetValue(itemWebResource, urlToWebValue.Value))
                    {
                        returnVal.Add(itemWebResource);
                    }
                }
                else
                {
                    _consoleHelper.PrintError($"The webresource url was not found! {urlToWebValue.Key}");
                }
            }
            return returnVal;
        }

        private bool IsValueSmallerOrEqualThanTargetValue(ItemWebResource itemWebResource, string webPageValue)
        {
            webPageValue = String.Concat(webPageValue.Where(c => !Char.IsWhiteSpace(c)));
            var regexPattern = itemWebResource.regexPricePattern;
            var targetPrice = itemWebResource.priceGoal;

            var regex = new Regex(regexPattern);
            var match = regex.Match(webPageValue);

            if (match.Success && double.TryParse(match.Groups[1].Value, out double extractedPrice))
            {
                return extractedPrice <= targetPrice;
            }

            _consoleHelper.PrintError($"The webresource url was not found! {itemWebResource.url}");
            return false;
        }
    }
}
