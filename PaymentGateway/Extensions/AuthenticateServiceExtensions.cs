using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PaymentGateway.Entities.DTOs;
using System.Text;

namespace PaymentGateway.Extensions;

public static class AuthenticateServiceExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
    {
        var jwtOptions = config.GetSection("Jwt").Get<JwtOptions>();
        services.AddSingleton(jwtOptions);

        services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
            };
        });

        return services;
    }
}