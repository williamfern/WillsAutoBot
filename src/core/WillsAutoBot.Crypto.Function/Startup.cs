using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WillsAutoBot.Core.Extensions;
using WillsAutoBot.Crypto.Services;
using WillsAutoBot.Data.Repositories;

[assembly: FunctionsStartup(typeof(WillsAutoBot.Crypto.Function.Startup))]

namespace WillsAutoBot.Crypto.Function
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        /// Configures the dependencies.
        /// </summary>
        /// <param name="builder">The web jobs host builder.</param>
        public override void Configure(IFunctionsHostBuilder builder)
        {

            // Configurations
            builder.Services.AddConfiguration();

            // Dependency Extensions
            builder.Services.AddAutoMapper();

            // Repositories
            builder.Services.AddScoped<ICoinRepository, CoinRepository>();
            builder.Services.AddScoped<ICoinPriceRepository, CoinPriceRepository>();

            // Services
            builder.Services.AddScoped<ICoinService, CoinService>();
        }
    }
}