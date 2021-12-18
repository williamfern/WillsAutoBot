    using System;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using WillsAutoBot.BTCMarket.Services.HttpClient;
    using WillsAutoBot.Crypto.Services;
    using WillsAutoBot.Data.Repositories.Market;
    using WillsAutoBot.Data.Repositories.Order;
    using WillsAutoBot.Data.Storage;
    using WillsAutoBot.Settings;
    using WillsAutoBot.WebApi.Config;

    namespace WillsAutoBot.WebApi
    {
        public class Startup
        {
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllers();
                
                var config = new MapperConfiguration(cfg => 
                    cfg.AddProfile<ApiMapperProfile>());
                
                var mapper = config.CreateMapper();
                services.AddSingleton(mapper);
                
                services.AddSingleton(new AzureStorageSettings(Configuration.GetValue<string>("WillsAutoBotStorage")));
                services.AddHttpClient();
                // clients
                services.AddScoped<IBtcMarketHttpClient, BtcMarketHttpClient>();
                // repos
                services.AddScoped<IOrderRepository,OrderRepository>();
                services.AddScoped<IMarketRepository,MarketRepository>();
                // services.AddScoped<ICoinRepository, CoinRepository>();
                // services
                services.AddScoped<IOrdersService,OrdersService>(); 
                services.AddScoped<ICoinService,CoinService>(); 
                //services.AddScoped<IMarketsService,IMarketsService>();
                
             
                // services.AddHttpClient<IBtcMarketHttpClient, BtcMarketHttpClient>(x =>
                // {
                //     x.BaseAddress = new Uri(baseUrl);
                //     x.DefaultRequestHeaders.Accept
                //         .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //     x.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
                //     x.DefaultRequestHeaders.Add("apikey", publicApiKey);
                //     x.DefaultRequestHeaders.Add("timestamp", GetNetworkTime().ToString());
                // });

                // config
                services.Configure<TableNameSettings>(Configuration.GetSection("TableNameSettings"));
                services.Configure<BtcMarketSettings>(Configuration.GetSection("BtcMarketSettings"));
                
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WillsAutoBot.WebApi", Version = "v1" });
                });
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WillsAutoBot.WebApi v1"));
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            }
            
            private static double GetNetworkTime()
            {
                TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
                long unixTime = (long)span.TotalMilliseconds;
                return unixTime;
            }
        }
    }