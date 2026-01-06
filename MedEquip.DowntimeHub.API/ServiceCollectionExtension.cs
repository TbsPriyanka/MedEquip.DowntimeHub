using MedEquip.DowntimeHub.Common.JwtHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace MedEquip.DowntimeHub.API
{
    public static class ServiceCollectionExtension
    {
        private const string BearerScheme = "Bearer";
        public static IServiceCollection WithAddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = BearerScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = BearerScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection WithJwtSetting(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSecretKey"));

            services.AddScoped<JwtTokenHelper>();

            var jwt = configuration
                .GetSection("JwtSecretKey")
                .Get<JwtSettings>();

            if (jwt == null)
                throw new InvalidOperationException("JwtSecretKey configuration section is missing.");

            if (string.IsNullOrWhiteSpace(jwt.SecretKey))
                throw new InvalidOperationException("JWT SecretKey missing.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issuer,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt.SecretKey)
                        ),

                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
