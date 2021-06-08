using course_backend.Abstractions.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class SpaInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddSpaStaticFiles(x =>
            {
                x.RootPath = "Frontend";
            });
        }
    }
}