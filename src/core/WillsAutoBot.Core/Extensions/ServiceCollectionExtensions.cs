using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WillsAutoBot.Core.Mappers;
using WillsAutoBot.Data.Storage;
using WillsAutoBot.Settings;
using WillsAutoBot.WebApi.Config;

namespace WillsAutoBot.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Azure Functions configuration to the <paramref name="services"/> collection,
        /// calling the <paramref name="configurationAction" /> action to complete any custom configuration.
        /// </summary>
        /// <param name="services">A services collection.</param>
        /// <param name="configurationAction">Any additional setup required.</param>
        public static IConfiguration AddConfiguration(this IServiceCollection services,
            Action<IConfigurationRoot> configurationAction = null)
        {
            string basePath = IsDevelopmentEnvironment() ?
                Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot") :
                $"{Environment.GetEnvironmentVariable("HOME")}\\site\\wwwroot";

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.shared.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton(new AzureStorageSettings(configuration.GetValue<string>("WillsAutoBotStorage")));
            
            services.Configure<TableNameSettings>(configuration.GetSection("TableNameSettings"));
            services.Configure<QueueNameSettings>(configuration.GetSection("QueueNameSettings"));
            services.Configure<BlobNameSettings>(configuration.GetSection("BlobNameSettings"));


            return configuration;
        }
        
        public static bool IsDevelopmentEnvironment()
        {
            return "Development".Equals(Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT"), StringComparison.OrdinalIgnoreCase);
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<CoinMapperProfile>(); 
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddScoped<IMapper, Mapper>();
        }

    }
}