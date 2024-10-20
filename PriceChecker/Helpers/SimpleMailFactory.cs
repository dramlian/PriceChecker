using System.Text;
using PriceChecker.Iterfaces;
using PriceChecker.Models;

namespace PriceChecker.Helpers
{
    public class SimpleMailFactory: IMailFactory
    {
        IEnumerable<ItemWebResource> _webResources;

        public SimpleMailFactory(IEnumerable<ItemWebResource> webResources)
        {
            _webResources = webResources;
        }

        public string CreateMail()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <title>Simple Styled Table</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine("        table {");
            sb.AppendLine("            width: 50%;");
            sb.AppendLine("            margin: 20px auto;");
            sb.AppendLine("            border-collapse: collapse; /* Merge borders */");
            sb.AppendLine("        }");
            sb.AppendLine("        th, td {");
            sb.AppendLine("            border: 1px solid #333; /* Border color */");
            sb.AppendLine("            padding: 8px; /* Space inside cells */");
            sb.AppendLine("            text-align: left; /* Align text to the left */");
            sb.AppendLine("        }");
            sb.AppendLine("        th {");
            sb.AppendLine("            background-color: #f2f2f2; /* Light gray background for header */");
            sb.AppendLine("        }");
            sb.AppendLine("        tr:nth-child(even) {");
            sb.AppendLine("            background-color: #f9f9f9; /* Light gray for even rows */");
            sb.AppendLine("        }");
            sb.AppendLine("        tr:hover {");
            sb.AppendLine("            background-color: #e0e0e0; /* Highlight row on hover */");
            sb.AppendLine("        }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine();
            sb.AppendLine("<table>");
            sb.AppendLine("  <tr>");
            sb.AppendLine("    <th>Url</th>");
            sb.AppendLine("    <th>Your target price</th>");
            sb.AppendLine("    <th>The actual price</th>");
            sb.AppendLine("  </tr>");
            foreach(var webResource in _webResources)
            {
                if (webResource.wasThePriceGoalHit)
                {
                    sb.AppendLine("<tr style=\"background-color: lightgreen;\">");
                }
                else
                {
                    sb.AppendLine("<tr>");
                }
                sb.AppendLine($"    <td>{webResource.url}</td>");
                sb.AppendLine($"    <td>{webResource.priceGoal}</td>");
                if (webResource is FailedItemWebResource)
                {
                    sb.AppendLine($"    <td style=\"background-color: red;\"> N/A </td>");
                }
                else
                {
                    sb.AppendLine($"    <td>{webResource.actualPrice}</td>");
                }
                sb.AppendLine("  </tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine();
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            return sb.ToString();
        }

    }
}
