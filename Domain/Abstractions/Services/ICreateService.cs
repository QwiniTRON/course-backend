using System;

namespace Domain.Abstractions.Services
{
    public interface ICreateService
    {
        object Create<TServiceType>(IServiceProvider services);
    }
}