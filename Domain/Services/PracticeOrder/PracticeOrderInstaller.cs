using course_backend.Abstractions.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domain.Services
{
    public class PracticeOrderInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddScoped<IPracticeOrderProvider, PracticeOrderProviderService>();
        }
    }
}