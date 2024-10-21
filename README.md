### Price Checker ###

The tool takes a config in a format (see the InputConfig.xml file) and
checks a price of that item on the url you provided.
Important note is that you need to have mailjet smtp account set up with
private and public key added preferably to your host machine system variables
under names PUBLIC_KEY_PARSERTOOL and PRIVATE_KEY_PARSERTOOL respectively.

The outcome of the tool is an email sent with a table that gives you
your target price, the actual price and information wether the pricegoal
was hit. If there was a problem with the value parsing that is also
represented in the mail.

