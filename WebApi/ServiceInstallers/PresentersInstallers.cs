using System;
using System.Linq;
using course_backend.Abstractions.DI;
using course_backend.Presentation;
using Domain.Abstractions.Outputs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.ServiceInstallers
{
    public class PresentersInstallers: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {
            typeof(IOutput).Assembly.ExportedTypes
                .Where(x => typeof(IOutput).IsAssignableTo(x) && !x.IsInterface && !x.IsAbstract)
                .ToList()
                .ForEach(output =>
                {
                    var presenterType = typeof(IPresenter<>).MakeGenericType(output);

                    var presenter = typeof(IPresenter<>).Assembly.GetTypes()
                        .FirstOrDefault(x => presenterType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

                    if (presenter is null)
                    {
                        throw new Exception($"Presenter wasn't found for {output.Name}");
                    }

                    serviceCollection.AddScoped(presenterType, presenter);
                });
        }
    }
}