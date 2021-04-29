using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using course_backend.Abstractions.DI;
using Domain.Abstractions.Services.IMailClient;

namespace course_backend.Services.MailClient
{
    public class MailClient : IMailClient, IInitializeable<MailClientOptions>
    {
        public MailClient() {}

        public void InitializeOptions(MailClientOptions options)
        {
            SMTPPort = options.SMTPPort;
            SMTPProvider = options.SMTPProvider;
            CredentialMail = options.CredentialMail;
            CredentialPassword = options.CredentialPassword;
        }
        
        public int SMTPPort { get; set; }
        public string SMTPProvider { get; set; }
        public string CredentialMail { get; set; }
        public string CredentialPassword { get; set; }

        /// <summary>
        ///     Send mail to destination with configured message
        ///
        ///     Configure message like this:
        ///     m.Subject = "Theme for letter";
        ///     m.Body = "<h2>Html content</h2>";
        /// </summary>
        /// <param name="fromAdress">Address initiator</param>
        /// <param name="toAdress">receiver for this letter</param>
        /// <param name="messageBuilder">builder delegate to configure message</param>
        public async Task SendMail(string fromAdress, string toAdress, Action<MailMessage> messageBuilder)
        {
            try
            {
                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress(fromAdress, "CourseBackendMailClient");
                // кому отправляем
                MailAddress to = new MailAddress(toAdress);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                messageBuilder(m);
                m.IsBodyHtml = true;
                
                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient(SMTPProvider, SMTPPort);
                // логин и пароль
                smtp.Credentials = new NetworkCredential(CredentialMail, CredentialPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(m);
            }
            catch (Exception e){}
        }
    }
}