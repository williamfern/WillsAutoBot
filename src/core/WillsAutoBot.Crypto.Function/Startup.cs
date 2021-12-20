using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using WillsAutoBot.Core.Extensions;

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

            // Repositories

            // Services

        }
    }
}