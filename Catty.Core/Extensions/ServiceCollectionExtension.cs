using Catty.Core.Models;
using Catty.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json;

namespace Catty.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPrivateKeyAuthentication(this IServiceCollection services, bool allow = false)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            if (allow)
                services.AddAuthentication(CoreConstants.AuthenticationScheme.PrivateKey).AddScheme<AuthenticationSchemeOptions, PrivateAuthHandler>(CoreConstants.AuthenticationScheme.PrivateKey, null, null);

            return services;
        }

        public static IServiceCollection AddAppSwaggerGen(this IServiceCollection services, string scheme, string projectName)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{projectName.Split('.')[0]} Api", // can be dynamic
                    Description = $"{projectName.Split('.')[0]} Api - A mongoDB crud api for managing cats.", // can be dynamic
                    Version = "v1"
                });

                c.AddSecurityDefinition(scheme, new OpenApiSecurityScheme
                {
                    Description = $"Enter '[scheme] [TokenValue]' inside the text field below.<br/> Example: {scheme} 7d112eaf23",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = scheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = scheme
                            },
                            Scheme = "oauth2",
                            Name = scheme,
                            In = ParameterLocation.Header
                        },
                        Array.Empty<string>()
                    }
                });

                c.DocumentFilter<SwaggerDocumentFilter>();

                var xmlFilename = $"{projectName}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IServiceCollection AddAppControllers(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.WriteIndented = true;
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
               // TODO:
            });

            return services;
        }
    }
}
