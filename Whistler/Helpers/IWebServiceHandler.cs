using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Helpers
{
    interface IWebServiceHandler
    {
        Task<string> GetResponseThroughGet(string url, bool requiresAuthorization);
        Task<string> GetResponseThroughPost(string url, bool requiresAuthorization);
    }
}
