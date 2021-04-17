using course_backend.Abstractions.DI;
using Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            serviceCollection.Configure<AuthOptions>(configuration.GetSection("Auth"));


            AuthOptions authConfig = configuration.GetSection("Auth").Get<AuthOptions>();

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
        }
    }
}