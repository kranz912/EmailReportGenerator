using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
namespace EmailReportGenerator.Helpers
{
    class SendEmailHelper :IDisposable
    {

        SmtpClient smtpClient;
        public SendEmailHelper(string server, string host, int port)
        {
            smtpClient = new SmtpClient();
            smtpClient.Host = server;
            smtpClient.Port = port;
            smtpClient.EnableSsl = true;
        }
        public async Task SendEmail(string sender,string password, List<string> receiverList, List<string> ccList =null,string subject = null, string body =null, string attachment =null)
        {
            try {
                    if (sender!=null) {
                    MailMessage message = new MailMessage(sender,receiverList[0], subject, body);
                    receiverList.RemoveAt(0);

                    foreach (var receiver in receiverList)
                    {

                        message.To.Add(receiver);
                    }
                    if (ccList != null)
                    {
                        foreach (var cc in ccList)
                        {
                            message.CC.Add(cc);
                        }
                    }

                    if (attachment != null)
                    {
                        Attachment data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                        message.Attachments.Add(data);
                    }
                    smtpClient.Credentials = new NetworkCredential(sender, password);
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            
        }


        public void Dispose()
        {
            smtpClient.Dispose();
        }
    }
}
