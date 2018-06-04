using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Helpers
{
    class HttpHelper
    {
        public async Task GetHttpContentAsync(string url, string token, string filename = null)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage responseMessage;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                responseMessage = await httpClient.SendAsync(request);
                if (filename != null)
                {
                    var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                    await responseMessage.Content.CopyToAsync(fileStream)
                            .ContinueWith(
                               (copytask) =>
                               {
                                   fileStream.Close();
                               }
                            );
                }
                Console.Write(filename);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
