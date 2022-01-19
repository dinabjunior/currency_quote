using System.IO.Compression;
using AutoMapper;
using Br.Com.Company.CurrencyQuote.Common.Infraestructure.Filters;
using Br.Com.Company.CurrencyQuote.Data.Infraestructure.Data;
using Br.Com.Company.CurrencyQuote.Data.Persistence.Repository;
using Br.Com.Company.CurrencyQuote.Domain.Dtos;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Br.Com.Company.CurrencyQuote.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SegmentRateDto).Assembly);
            services.AddDbContext<DataContext>();

            services
                .AddResponseCompression()
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                });

            services.AddData<DataContext>(Configuration);
            services.AddDomainServices();
            services.AddCommonServices();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add<NotificationFilter>();
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<IRepository>();
                    fv.RegisterValidatorsFromAssemblyContaining<SegmentRateDto>();
                    fv.ConfigureClientsideValidation(enabled: false);
                    fv.LocalizationEnabled = false;
                });

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddCustomSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper)
        {
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomSwagger();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
