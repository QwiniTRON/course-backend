using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Domain.Abstractions.Services.IMailClient
{
    public interface IMailClient
    {
        Task SendMail(string to, Action<MailMessage> messageBuilder);
    }
}