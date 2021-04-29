using System;
using Domain.Abstractions.Services;
using Domain.Abstractions.Services.IMailClient;
using Microsoft.Extensions.DependencyInjection;

namespace course_backend.Services.MailClient
{
    public class MailClientOptions
    {
        public int SMTPPort { get; set; } = 587;
        public string SMTPProvider { get; set; } = "smtp.gmail.com";
        public string CredentialMail { get; set; }
        public string CredentialPassword { get; set; }

        public MailClientOptions() {}
    }
    
    public static class MailClientExtension
    {
        /// <summary>
        ///     Add mail client to services
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="builder">function to configure option object</param>
        /// <typeparam name="TService">type which will be registered for mail client</typeparam>
        public static IServiceCollection AddMailClient(this IServiceCollection services, Action<MailClientOptions> builder)
        {
            var createFunc = new Func<IServiceProvider, MailClient>(provider =>
            {
                var serviceCreator = provider.GetService<ICreateService>();

                if (serviceCreator is null)
                {
                    throw new Exception("serviceCreator is null");
                }
                
                var mailClient = serviceCreator.Create<MailClient>(provider);

                if (mailClient is null)
                {
                    throw new Exception("MailClient is null");
                }

                if (!(mailClient is MailClient mailClientService))
                {
                    throw new Exception("MailClient is null after cast");
                }
                
                var clientOptions = new MailClientOptions();
                builder(clientOptions);
                
                mailClientService.InitializeOptions(clientOptions);
                
                return mailClientService;
            });
            services.AddScoped<MailClient, MailClient>(createFunc);
            services.AddScoped<IMailClient, MailClient>(createFunc);
            
            return services;
        } 
    }
}