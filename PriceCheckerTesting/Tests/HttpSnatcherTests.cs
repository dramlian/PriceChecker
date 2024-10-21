using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Helpers;
using PriceCheckerTesting.TestHelpers;
using Xunit;

namespace PriceCheckerTesting.Tests
{
    public class HttpSnatcherTests
    {
        readonly HttpSnatcher _snatcher;

        public HttpSnatcherTests()
        {
            _snatcher = new HttpSnatcher(new HttpClient(),new TestConsoleLogger());
        }
        [Fact]
        public async Task DataAreFetchedWithNoExceptionAndLinkedProperly()
        {
           var data= await _snatcher.GetUrls(new List<string> { "https://google.com", "https://facebook.com" });
           Assert.NotNull(data["https://google.com"]);
           Assert.NotNull(data["https://facebook.com"]);
           Assert.Contains("google", data["https://google.com"]);
           Assert.Contains("facebook", data["https://facebook.com"]);
        }
    }
}
