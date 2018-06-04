using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using EmailReportGenerator.Helpers;
using System.IO;

namespace EmailReportGenerator
{
    class Program
    {

        public static async Task Main(string[] args)
        {
            List<string> scopes = new List<string>()
            {
                "user.read",
                "Reports.Read.All"
            };
            string token = await Authentication.getTokenAuthAsync(scopes);
            Console.WriteLine(token);
            args[0] = "D7";
            if (args.Length != 0)
            {
                string graphAPIEndpoint = $"https://graph.microsoft.com/v1.0/reports/getEmailActivityUserDetail(period='{args[0]}')";
                Console.Write(graphAPIEndpoint);
                HttpHelper httpHelper = new HttpHelper();
                string filepath = Path.GetTempPath() + $"\\MailUsageReport{args[0]}.csv";
                await httpHelper.GetHttpContentAsync(graphAPIEndpoint, token, filepath);
                await CSVHelper.sortCSV(filepath);
                SendEmailHelper emailHelper = new SendEmailHelper(System.Configuration.ConfigurationManager.AppSettings["Server"], System.Configuration.ConfigurationManager.AppSettings["Server"], 587);
                List<string> receivers =System.Configuration.ConfigurationManager.AppSettings["Receivers"].Split(',').ToList();
                List<string> ccs = System.Configuration.ConfigurationManager.AppSettings["CCs"].Split(',').ToList();
                await emailHelper.SendEmail(System.Configuration.ConfigurationManager.AppSettings["Email"], System.Configuration.ConfigurationManager.AppSettings["Password"], receivers, ccs, args[0]=="D7"?"Weekly Report Mail Usage":"Monthly Report Mail Usage", args[0] == "D7" ? "Weekly Report Mail Usage" : "Monthly Report Mail Usage", filepath);

            }
        }
    }
}
