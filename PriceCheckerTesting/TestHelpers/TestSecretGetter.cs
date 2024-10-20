using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceChecker.Iterfaces;

namespace PriceCheckerTesting.TestHelpers
{
    public class TestSecretGetter : ISecretGetter
    {
        public string GetPublicKey()
        {
            return string.Empty;
        }

        public string GetSecretKey()
        {
            return string.Empty;
        }
    }
}
