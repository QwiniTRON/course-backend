using course_backend.Abstractions.DI;
using course_backend.Services.MailClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class MailClientInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddMailClient(options =>
            {
                options.CredentialMail = configuration.GetSection("MailClient:Mail").Value;
                options.CredentialPassword = configuration.GetSection("MailClient:Password").Value;
            });
        }
    }
}