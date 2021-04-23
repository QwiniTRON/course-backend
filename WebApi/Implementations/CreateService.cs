using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Services;

namespace course_backend.Implementations
{
    public class CreateService : ICreateService
    {
        /* find all required services and create the wanted service */
        public object Create<TServiceType>(IServiceProvider services)
        {
            try
            {
                var serviceType = typeof(TServiceType);

                var serviceConstructor = serviceType.GetConstructors().First();

                List<object> servicesForConstructor = new List<object>();
            
                foreach (var parameterInfo in serviceConstructor.GetParameters())
                {
                    var service = services.GetService(parameterInfo.ParameterType);
                
                    servicesForConstructor.Add(service);
                }

                return serviceConstructor.Invoke(servicesForConstructor.ToArray());
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}