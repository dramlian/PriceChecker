namespace PriceChecker.Models
{
    public class FailedItemWebResource : ItemWebResource
    {
        public FailedItemWebResource(ItemWebResource itemWebResource)
            : base(itemWebResource.url, itemWebResource.regexPricePattern, itemWebResource.priceGoal) // Assuming these properties exist
        {
            actualPrice = null; // Or any other logic you want to apply
        }

    }
}
