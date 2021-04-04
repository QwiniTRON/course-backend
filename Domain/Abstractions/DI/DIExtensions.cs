using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace course_backend.Abstractions.DI
{
    public static class DIExtensions
    {
        public static void  BuildFromAssambly(
            this IServiceCollection services, 
            Type typeFromTargetAssembly, 
            IConfiguration configuration,
            IHostEnvironment env)
        {
            Type[] targetAssamblyTypes = typeFromTargetAssembly.Assembly.GetTypes();
            targetAssamblyTypes.Where((type) =>
                    type.IsAssignableTo(typeof(IInjectable)) && !type.IsInterface && !type.IsAbstract)
                    .Select(Activator.CreateInstance)
                    .Cast<IInjectable>()
                    .ToList()
                    .ForEach((service) => service.Inject(services, configuration, env));
        }
    }
}