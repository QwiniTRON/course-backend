using System.Text;
using System.Threading.Tasks;
using course_backend.Abstractions.DI;
using course_backend.Identity;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace course_backend.ServiceInstallers
{
    public class AuthInstaller: IInjectable
    {
        public void Inject(IServiceCollection serviceCollection, IConfiguration configuration, IHostEnvironment env)
        {         
            AuthOptions authConfig = configuration.GetSection("Auth").Get<AuthOptions>();

            serviceCollection.AddScoped<IAuthDataProvider, AuthDataProvider>();

            serviceCollection.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            serviceCollection.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<AppDbContext>();
            
            serviceCollection
                .AddAuthentication(opts =>
                {
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                    };
                    
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authConfig.ISSUER,
                        ValidAudience = authConfig.AUDIENCE,
                        IssuerSigningKey = authConfig.GetSymmetricAlgorithmKey(),
                    };
                });
        }
    }
}