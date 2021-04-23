using course_backend.Abstractions.DI;
using course_backend.Services;
using Domain.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class FileUploaderInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            var defaultFileFolder = configuration.GetSection("Static:DefaultFileFolder").Value;
            
            serviceCollection.AddFileAploader(defaultFileFolder);
        }
    }
}