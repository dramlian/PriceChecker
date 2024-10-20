using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Iterfaces;

namespace PriceCheckerTesting.TestHelpers
{
    public class TestHttpSnatcher : IResourceSnatcher
    {
        public async Task<Dictionary<string, string>> GetUrls(IEnumerable<string>? urls)
        {
            await Task.Delay(5);
            var result = new Dictionary<string, string>();
            urls ??= new List<string>() {"http://example.com/page1", "http://example.com/page2", "http://example.com/page3"};
            var urlsArr=urls.ToArray();

            for(int i = 0; i < urlsArr.Count(); i++)
            {
                result[urlsArr[i]] = $"<html><body><h1>Page 1</h1><p>This is the content of page {i}.</p></body></html>";
            }

            return result;
        }
    }
}
