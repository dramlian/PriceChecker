namespace PriceChecker.Models
{
    public class ItemWebResource
    {
        public string url;
        public string regexPricePattern;
        public double priceGoal;
        public double? actualPrice;
        public bool wasThePriceGoalHit;

        public ItemWebResource(string url, string regexPricePattern, double priceGoal)
        {
            this.url = url;
            this.regexPricePattern = regexPricePattern;
            this.priceGoal = priceGoal;
        }

    }

}
