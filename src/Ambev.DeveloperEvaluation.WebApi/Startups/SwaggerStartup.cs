using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ambev.DeveloperEvaluation.WebApi.Startups
{
    internal static class SwaggerStartup
    {
        public static void AddSwagger(
            this IServiceCollection services,
            string title,
            string version)
        {
            var options = new SwaggerOptions();

            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                });

                swaggerOptions.AddSecurity();
            });
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(o =>
            {
                o.PreSerializeFilters.Add(UseLowerCaseRoutingConventionFilter);
            });
            app.UseSwaggerUI();
        }

        private static void AddSecurity(this SwaggerGenOptions swaggerOptions)
        {
            const string bearer = "Bearer";

            swaggerOptions.AddSecurityRequeriment(bearer);
            swaggerOptions.AddSecurityDefinition(bearer, new OpenApiSecurityScheme
            {
                Description = "Inform the access token.",
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http,
            });
        }

        private static void AddSecurityRequeriment(this SwaggerGenOptions swaggerOptions, string id)
        {
            swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = id,
                        },
                    },
                    Array.Empty<string>()
                },
            });
        }

        private static void UseLowerCaseRoutingConventionFilter(OpenApiDocument document, HttpRequest request)
        {
            var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
            document.Paths.Clear();

            foreach (var pathItem in paths)
            {
                document.Paths.Add(pathItem.Key, pathItem.Value);
            }
        }
    }
}
