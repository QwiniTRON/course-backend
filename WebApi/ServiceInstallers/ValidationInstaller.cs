using course_backend.Abstractions.DI;
using Domain.Abstractions;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class ValidationInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection
                .AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => 
                    fv.RegisterValidatorsFromAssembly(typeof(IEntity).Assembly));
        }
    }
}