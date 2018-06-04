using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Helpers
{
    class Authentication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static async Task<string> getTokenAuthAsync(List<string> scopes)
        {
            AuthenticationResult authResult = null;
            string token = null;
            try {
                var a= ApplicationGlobal.PublicClientApp.Users.FirstOrDefault();
                authResult = await ApplicationGlobal.PublicClientApp.AcquireTokenSilentAsync(scopes, ApplicationGlobal.PublicClientApp.Users.FirstOrDefault());

            }
            catch (MsalUiRequiredException e)
            {
                try
                {
                    authResult = await ApplicationGlobal.PublicClientApp.AcquireTokenAsync(scopes);
                }
                catch (Exception)
                {
                   
                    log.Error(e.Message);
                }
            }
            catch(Exception e)
            {
                log.Error(e.Message);
            }
            token = authResult.AccessToken;
            return token;

        }
    }
}
