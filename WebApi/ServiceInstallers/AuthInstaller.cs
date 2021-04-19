using course_backend.Abstractions.DI;
using course_backend.Identity;
using Domain.Abstractions.Services;
using Domain.Data;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authConfig.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = authConfig.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = authConfig.GetSymmetricAlgorithmKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            serviceCollection.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 4;
            }).AddEntityFrameworkStores<AppDbContext>();
        }
    }
}