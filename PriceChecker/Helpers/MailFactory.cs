using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class MailFactory
    {
        IEnumerable<ItemWebResource> _webResources;

        public MailFactory(IEnumerable<ItemWebResource> webResources)
        {
            _webResources = webResources;
        }

        public string CreateMail()
        {
            //TODO Create a table with previous price and new price that we are alerting (abo tak)
            string urls = string.Join(",", _webResources.Select(x => x.url).ToArray());
            return $"You need to check these links that have your target prices: \n {urls}";
        }

        //public string CreateMail()
        //{
        //    return "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Simple Styled Table</title>\r\n    <style>\r\n        table {\r\n            width: 50%;\r\n            margin: 20px auto;\r\n            border-collapse: collapse; /* Merge borders */\r\n        }\r\n        th, td {\r\n            border: 1px solid #333; /* Border color */\r\n            padding: 8px; /* Space inside cells */\r\n            text-align: left; /* Align text to the left */\r\n        }\r\n        th {\r\n            background-color: #f2f2f2; /* Light gray background for header */\r\n        }\r\n        tr:nth-child(even) {\r\n            background-color: #f9f9f9; /* Light gray for even rows */\r\n        }\r\n        tr:hover {\r\n            background-color: #e0e0e0; /* Highlight row on hover */\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<table>\r\n  <tr>\r\n    <th>Name</th>\r\n    <th>Age</th>\r\n    <th>City</th>\r\n  </tr>\r\n  <tr>\r\n    <td>John</td>\r\n    <td>25</td>\r\n    <td>New York</td>\r\n  </tr>\r\n  <tr>\r\n    <td>Alice</td>\r\n    <td>30</td>\r\n    <td>Los Angeles</td>\r\n  </tr>\r\n  <tr>\r\n    <td>Bob</td>\r\n    <td>22</td>\r\n    <td>Chicago</td>\r\n  </tr>\r\n</table>\r\n\r\n</body>\r\n</html>";
        //}
    }
}
