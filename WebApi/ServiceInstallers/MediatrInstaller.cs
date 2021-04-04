using course_backend.Abstractions.DI;
using course_backend.Implementations;
using Domain.Abstractions.Outputs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.Presentation.Presenters
{
    public class MediatrInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            serviceCollection.AddMediatR(x => 
                x.AsScoped(), typeof(IOutput).Assembly, typeof(Startup).Assembly);

            serviceCollection.AddScoped<IUseCaseDispatcher, UseCaseDispatcher>();
        }
    }
}