using PriceChecker.Models;

namespace PriceCheckerTesting.TestHelpers
{
    public class TestWebResourceFactory
    {
        public IEnumerable<ItemWebResource> GetDummyData(int count)
        {
            List<ItemWebResource> dummyData = new ();

            for (int i = 0; i < count; i++) 
            {
                dummyData.Add(new ItemWebResource($"www.url{i}.com", @"page(\d+)", 50+i));
            }
            return dummyData;
        }

        public IEnumerable<FailedItemWebResource> GetFailedDummyData(int count)
        {
            List<FailedItemWebResource> dummyData = new();

            for (int i = 0; i < count; i++)
            {
                dummyData.Add(new FailedItemWebResource(new ItemWebResource($"www.url{i}.com", string.Empty, 50 + i)));
            }
            return dummyData;
        }

    }
}
