using System;
using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace course_backend.Services
{
    public static class FileUploaderExstension
    {
        public static IServiceCollection AddFileAploader(this IServiceCollection services, string fileFolder)
        {
            
            services.AddScoped<IFileUploader, FileUploader>(provider =>
            {
                var serviceCreator = provider.GetService<ICreateService>();

                var fileUploader = serviceCreator.Create<FileUploader>(provider);

                if (fileUploader is null)
                {
                    throw new Exception("fileUploader is null");
                }

                if (!(fileUploader is FileUploader fileUploaderService))
                {
                    throw new Exception("fileUploader is null after cast");
                }

                fileUploaderService.SetFileFolderDefault(fileFolder);
                
                return fileUploaderService;
            });

            return services;
        }
    }
}