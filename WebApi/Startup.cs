using System.IO;
using course_backend.Abstractions.DI;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace course_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Env = env;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private IHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* build infrastructure services */
            services.BuildFromAssambly(typeof(AppDbContext), Configuration, Env);
            /* build WebApi */
            services.BuildFromAssambly(typeof(Startup), Configuration, Env);
            /* build domain */
            services.BuildFromAssambly(typeof(DIExtensions), Configuration, Env);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "course_backend v1"));
            }

            var defaultFileFolder = Configuration.GetSection("Static:DefaultFileFolder").Value;
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.MapWhen(route =>
            {
                return route.Request.Path.StartsWithSegments("/swagger") == false;
            }, builder =>
            {
                builder.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(env.WebRootPath, defaultFileFolder))
                });
                builder.UseSpa(config =>
                {
                    config.Options.DefaultPage = "/Frontend/index.html";
                    config.Options.SourcePath = "wwwroot";
                });
            });
        }
    }
}