using System.Text.RegularExpressions;
using PriceChecker.Iterfaces;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class PriceComparer
    {
        IResourceSnatcher _httpHelper;
        IEnumerable<ItemWebResource> _webResources;
        IAppLogger _logger;

        public PriceComparer(IResourceSnatcher httpHelper, IEnumerable<ItemWebResource> webResources, IAppLogger logger)
        {
            _httpHelper= httpHelper;
            _webResources = webResources;
            _logger= logger;
        }

        public async Task<IEnumerable<ItemWebResource>> FetchAndComparePrices()
        {
            var urls= _webResources.Select(x=>x.url) ?? Enumerable.Empty<string>();
            return ParsePriceAndDetermineWebResourceType(await _httpHelper.GetUrls(urls));
        }

        private IEnumerable<ItemWebResource> ParsePriceAndDetermineWebResourceType(Dictionary<string, string> urlsToWebValue)
        {
            List<ItemWebResource> returnVal = new (); 
            foreach(var urlToWebValue in urlsToWebValue)
            {
                var itemWebResource = _webResources.Where(x => x.url.Equals(urlToWebValue.Key)).First();
                if (itemWebResource is null) { throw new Exception("Problem with the url to webresource relationship"); }

                itemWebResource.actualPrice = GetTheActualPriceOfItem(itemWebResource.regexPricePattern ?? string.Empty, urlToWebValue.Value, out bool parsingProblem);

                if (parsingProblem)
                {
                    _logger.AlertFailure($"Problem with parsing the value: {urlToWebValue.Key}");
                    returnVal.Add(new FailedItemWebResource(itemWebResource));
                }
                else
                {
                    if (itemWebResource.actualPrice <= itemWebResource.priceGoal)
                    {
                        itemWebResource.wasThePriceGoalHit = true;
                    }
                    returnVal.Add(itemWebResource);
                }

            }
            return returnVal;
        }

        private double GetTheActualPriceOfItem(string regexPattern, string webPageValue, out bool parsingProblem)
            //the bool indicates problem with parsing
        {
            webPageValue = String.Concat(webPageValue.Where(c => !Char.IsWhiteSpace(c)));

            var regex = new Regex(regexPattern);
            var match = regex.Match(webPageValue);

            if (match.Success && double.TryParse(match.Groups[1].Value.Replace(',', '.'), out double extractedPrice))
            {
                parsingProblem = false;
                return extractedPrice;
            }
            else
            {
                parsingProblem = true;
                return 0;
            }
        }
    }
}
