using System;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class StartupExtensions
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddFluentValidationRulesToSwagger();
            services.AddSwaggerGen(setup =>
            {
                setup.IgnoreObsoleteActions();
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Company | Currency Quota API",
                    Description = "API Rest of foreign currency quote to BRL Real",
                    Contact = new OpenApiContact { Name = "Company", Url = new Uri("http://www.company.com.br") },
                    Version = "v1",
                });

                setup.CustomSchemaIds(t => t.FullName);
            }).AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
