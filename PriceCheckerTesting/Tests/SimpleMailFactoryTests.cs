using PriceChecker.Helpers;
using PriceCheckerTesting.TestHelpers;
using Xunit;

namespace PriceCheckerTesting.Tests
{
    public class SimpleMailFactoryTests
    {
        TestWebResourceFactory _webResourceFactory;
        SimpleMailFactory? _simpleMailFactory; 

        public SimpleMailFactoryTests()
        {
            _webResourceFactory=new TestWebResourceFactory();
        }

        [Fact]
        public void WebResourcesThatParsedProperly()
        {
            string expectedHtml = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Simple Styled Table</title>\r\n    <style>\r\n        table {\r\n            width: 50%;\r\n            margin: 20px auto;\r\n            border-collapse: collapse; /* Merge borders */\r\n        }\r\n        th, td {\r\n            border: 1px solid #333; /* Border color */\r\n            padding: 8px; /* Space inside cells */\r\n            text-align: left; /* Align text to the left */\r\n        }\r\n        th {\r\n            background-color: #f2f2f2; /* Light gray background for header */\r\n        }\r\n        tr:nth-child(even) {\r\n            background-color: #f9f9f9; /* Light gray for even rows */\r\n        }\r\n        tr:hover {\r\n            background-color: #e0e0e0; /* Highlight row on hover */\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<table>\r\n  <tr>\r\n    <th>Url</th>\r\n    <th>Your target price</th>\r\n    <th>The actual price</th>\r\n  </tr>\r\n<tr>\r\n    <td>www.url0.com</td>\r\n    <td>50</td>\r\n    <td></td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url1.com</td>\r\n    <td>51</td>\r\n    <td></td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url2.com</td>\r\n    <td>52</td>\r\n    <td></td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url3.com</td>\r\n    <td>53</td>\r\n    <td></td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url4.com</td>\r\n    <td>54</td>\r\n    <td></td>\r\n  </tr>\r\n</table>\r\n\r\n</body>\r\n</html>\r\n";
            _simpleMailFactory = new SimpleMailFactory(_webResourceFactory.GetDummyData(5));
            Assert.Equal(expectedHtml, _simpleMailFactory.CreateMail());
        }

        [Fact]
        public void WebResourcesThatParsedPoorly()
        {
            string expectedHtml = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Simple Styled Table</title>\r\n    <style>\r\n        table {\r\n            width: 50%;\r\n            margin: 20px auto;\r\n            border-collapse: collapse; /* Merge borders */\r\n        }\r\n        th, td {\r\n            border: 1px solid #333; /* Border color */\r\n            padding: 8px; /* Space inside cells */\r\n            text-align: left; /* Align text to the left */\r\n        }\r\n        th {\r\n            background-color: #f2f2f2; /* Light gray background for header */\r\n        }\r\n        tr:nth-child(even) {\r\n            background-color: #f9f9f9; /* Light gray for even rows */\r\n        }\r\n        tr:hover {\r\n            background-color: #e0e0e0; /* Highlight row on hover */\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<table>\r\n  <tr>\r\n    <th>Url</th>\r\n    <th>Your target price</th>\r\n    <th>The actual price</th>\r\n  </tr>\r\n<tr>\r\n    <td>www.url0.com</td>\r\n    <td>50</td>\r\n    <td style=\"background-color: red;\"> N/A </td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url1.com</td>\r\n    <td>51</td>\r\n    <td style=\"background-color: red;\"> N/A </td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url2.com</td>\r\n    <td>52</td>\r\n    <td style=\"background-color: red;\"> N/A </td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url3.com</td>\r\n    <td>53</td>\r\n    <td style=\"background-color: red;\"> N/A </td>\r\n  </tr>\r\n<tr>\r\n    <td>www.url4.com</td>\r\n    <td>54</td>\r\n    <td style=\"background-color: red;\"> N/A </td>\r\n  </tr>\r\n</table>\r\n\r\n</body>\r\n</html>\r\n";
            _simpleMailFactory = new SimpleMailFactory(_webResourceFactory.GetFailedDummyData(5));
            Assert.Equal(expectedHtml, _simpleMailFactory.CreateMail());
        }
    }
}
