using EmailReportGenerator.Helpers;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator
{
    public class ApplicationGlobal
    {
        static ApplicationGlobal()
        {
            _clientApp = new PublicClientApplication(ClientId, "https://login.microsoftonline.com/common", TokenCacheHelper.GetUserCache());
        }
        private static string ClientId = Environment.GetEnvironmentVariable("graphAPIClientID");
        private static PublicClientApplication _clientApp;

        public static PublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
