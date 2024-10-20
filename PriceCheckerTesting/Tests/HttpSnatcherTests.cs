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
        public void DataAreFetchedWithNoExceptionAndLinkedProperly()
        {

        }
    }
}
