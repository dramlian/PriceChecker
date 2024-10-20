using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceChecker.Iterfaces
{
    public interface IResourceSnatcher
    {
        public Task<Dictionary<string, string>> GetUrls(IEnumerable<string> urls);
    }
}
