using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.Abstractions.DI
{
    public interface IInjectable
    {
        void  Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env);
    }
}