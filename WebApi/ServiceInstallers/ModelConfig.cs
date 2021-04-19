using course_backend.Abstractions.DI;
using Infrastructure.Data.Configuration;
using Infrastructure.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class ModelConfig: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.Configure<AdminConfig>(configuration.GetSection("MainAdmin"));
            serviceCollection.Configure<AuthOptions>(configuration.GetSection("Auth"));
        }
    }
}