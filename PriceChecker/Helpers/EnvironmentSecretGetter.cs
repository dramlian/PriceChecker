using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Iterfaces;

namespace PriceChecker.Helpers
{
    public class EnvironmentSecretGetter : ISecretGetter
    {
        private IAppLogger messageLogger;

        public EnvironmentSecretGetter(IAppLogger messageLogger)
        {
            this.messageLogger = messageLogger;
        }

        public string GetPublicKey()
        {
            string? publicKey = Environment.GetEnvironmentVariable("PUBLIC_KEY_PARSERTOOL") ?? string.Empty;
            if(String.IsNullOrEmpty(publicKey))
            {
                messageLogger.AlertFailure("NO PUBLIC KEY FOUND");
            }
            return publicKey;
        }

        public string GetSecretKey()
        {
            string? publicKey = Environment.GetEnvironmentVariable("PRIVATE_KEY_PARSERTOOL") ?? string.Empty;
            if (String.IsNullOrEmpty(publicKey))
            {
                messageLogger.AlertFailure("NO PUBLIC KEY FOUND");
            }
            return publicKey;
        }
    }
}
