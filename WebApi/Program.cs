using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace course_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateHostBuilder(args).Build();

            using (var services = app.Services.CreateScope())
            {
                var context = services.ServiceProvider.GetService<AppDbContext>();
            
                context?.Database.Migrate();
            
                if (context is null) throw new NullReferenceException("context was null"); 
                
                /* if there is no one in the Db */
                if (context.Users.Any() == false)
                {
                    AppDbInitialize.Initialize(app.Services);
                }
            
                context.SaveChanges();
            }

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}