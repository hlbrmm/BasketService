using BasketService.Controllers;
using BasketService.Repositories;
using BasketService.Service;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BasketService
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
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<BasketController>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Basket API",
                    Description = "TYBootcamp Basket API",
                    Version = "v1"
                });
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddTransient<IBasketRepository, BasketRepository>();
            services.AddTransient<IBasketRepository, BasketRepository>();

            services.AddScoped<IBasketServiceV1, BasketServiceV1>();
            services.AddTransient<IBasketServiceV1, BasketServiceV1>();

            var redis = ConnectionMultiplexer.Connect("172.31.240.1");

            services.AddScoped(s => redis.GetDatabase());

            var producerConfig = new ProducerConfig();
            Configuration.Bind("producer", producerConfig);

            services.AddSingleton<ProducerConfig>(producerConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API");
            });
        }
    }
}
