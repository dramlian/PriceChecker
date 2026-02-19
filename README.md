# PriceChecker

A .NET tool that monitors product prices on the web and notifies you by email when your target price is hit.

## How It Works

1. Define items to track in `InputConfig.xml` — each entry has a URL, a target price, and a regex to extract the current price.
2. The tool fetches each page, parses the price, and compares it to your goal.
3. Every 30 minutes, it sends an email summary via **Mailjet SMTP** showing actual vs. target prices and whether each goal was reached.

## Configuration

```xml
<root>
    <Recipient>you@example.com</Recipient>
    <ItemWebResource>
        <url>https://example.com/product</url>
        <priceGoal>49.99</priceGoal>
        <regexPricePattern>price-regex</regexPricePattern>
    </ItemWebResource>
</root>
```

## Setup

Set your Mailjet SMTP keys as environment variables:

```
PUBLIC_KEY_PARSERTOOL=<your-public-key>
PRIVATE_KEY_PARSERTOOL=<your-private-key>
```

Then run:

```bash
dotnet run --project PriceChecker
```

