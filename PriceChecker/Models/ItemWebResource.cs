namespace PriceChecker.Models
{
    public class ItemWebResource
    {
        public string? url;
        public string? regexPricePattern;
        public double? priceGoal;
        double? actualPrice;

        public ItemWebResource(string url, string regexPricePattern, double priceGoal, double actualPrice)
        {
            this.url = url;
            this.regexPricePattern = regexPricePattern;
            this.priceGoal = priceGoal;
            this.actualPrice = actualPrice;
        }
    }

}
