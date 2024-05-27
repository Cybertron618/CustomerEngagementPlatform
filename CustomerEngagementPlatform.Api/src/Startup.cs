using Confluent.Kafka;
using CustomerEngagementPlatform.Api.Repositories;
using CustomerEngagementPlatform.Api.src.Caching;
using CustomerEngagementPlatform.Api.src.Data;
using CustomerEngagementPlatform.Api.src.EventHandling;
using CustomerEngagementPlatform.Api.src.Repositories;
using CustomerEngagementPlatform.Api.src.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerEngagementPlatform.Api.src
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add DbContext
            services.AddDbContext<CustomerEngagementContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Register repositories
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            // Register services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IActivityService, ActivityService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationService, NotificationService>();

            // Register Kafka Producer and Consumer
            services.AddSingleton<IKafkaProducer>(provider =>
                new KafkaProducer(Configuration["Kafka:BootstrapServers"]!, Configuration["Kafka:Topic"]!));

            services.AddSingleton<IKafkaConsumer>(provider =>
                new KafkaConsumer(Configuration["Kafka:BootstrapServers"]!, Configuration["Kafka:Topic"]!, Configuration["Kafka:GroupId"]!));

            // Register Redis Cache Service
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:Configuration"];
                options.InstanceName = Configuration["Redis:InstanceName"];
            });

            services.AddScoped<IRedisCacheService, RedisCacheService>();

            // Other services and configurations...
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerEngagementPlatform.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}