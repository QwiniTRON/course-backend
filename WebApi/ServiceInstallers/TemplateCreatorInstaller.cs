using course_backend.Abstractions.DI;
using course_backend.Services.TemplateCreator;
using Domain.Abstractions.Services.ITemplateCreator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class TemplateCreatorInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddScoped<ITemplateCreator, TemplateCreator>();
        }
    }
}