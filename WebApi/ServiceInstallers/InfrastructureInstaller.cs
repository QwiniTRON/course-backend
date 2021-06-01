using course_backend.Abstractions.DI;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class InfrastructureInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddScoped<IFileStorage, FileStorage>();
        }
    }
}