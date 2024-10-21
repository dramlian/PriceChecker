using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Helpers;
using PriceChecker.Iterfaces;
using PriceChecker.Models;
using PriceCheckerTesting.TestHelpers;
using Xunit;

namespace PriceChecker.Tests
{
    public class ConfigParserTests
    {
        readonly IAppLogger _appLogger;
        readonly ISecretGetter _secretGetter;
        readonly HttpClient _httpClient;
        readonly string _dummyFileContext;
        public ConfigParserTests()
        {
            _appLogger = new TestConsoleLogger();
            _secretGetter = new TestSecretGetter();
            _httpClient= new HttpClient();
            _dummyFileContext = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<root>\r\n    <Recipient>mail,mail</Recipient>\r\n    <ItemWebResource>\r\n        <url>google.com</url>\r\n        <priceGoal>100</priceGoal>\r\n        <regexPricePattern>\\d+</regexPricePattern>\r\n    </ItemWebResource>\r\n    <ItemWebResource>\r\n        <url>facebook.com</url>\r\n        <priceGoal>100</priceGoal>\r\n        <regexPricePattern>\\d+</regexPricePattern>\r\n    </ItemWebResource>\r\n</root>";
        }

        [Fact]
        public void Constructor_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
             Assert.Throws<FileNotFoundException>(() =>
                new ConfigParser(string.Empty, _secretGetter));
        }

        [Fact]
        public void FileExistsAndValueAreParsed()
        {
            string testConfigPath = "DummyConfig.xml";
            File.WriteAllText(testConfigPath, _dummyFileContext);
            var config=new ConfigParser(testConfigPath, _secretGetter).ParseTheValues();
            Assert.Equal(config.networkUsername, string.Empty);
            Assert.Equal(config.networkPassword, string.Empty);
            Assert.Equal(2, config.recipients.Count());
            Assert.Equal("mail", config.recipients.First());
            Assert.Equal("mail", config.recipients.Skip(1).Take(1).First());

            var webresource1 = config.webResources.First();
            Assert.Equal("google.com", webresource1.url);
            Assert.Equal(100, webresource1.priceGoal);
            Assert.Equal("\\d+", webresource1.regexPricePattern);

            var webresource2 = config.webResources.Skip(1).Take(1).First();
            Assert.Equal("facebook.com", webresource2.url);
            Assert.Equal(100, webresource2.priceGoal);
            Assert.Equal("\\d+", webresource2.regexPricePattern);

            File.Delete(testConfigPath);
        }

        
    }
}
