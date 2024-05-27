using CustomerEngagementPlatform.Api.Repositories;
using CustomerEngagementPlatform.Api.src.Caching;
using CustomerEngagementPlatform.Api.src.Data;
using CustomerEngagementPlatform.Api.src.EventHandling;
using CustomerEngagementPlatform.Api.src.Repositories;
using CustomerEngagementPlatform.Api.src.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DbContext
builder.Services.AddDbContext<CustomerEngagementContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register Kafka Producer and Consumer
builder.Services.AddSingleton<IKafkaProducer>(provider =>
    new KafkaProducer(builder.Configuration["Kafka:BootstrapServers"]!, builder.Configuration["Kafka:Topic"]!));

builder.Services.AddSingleton<IKafkaConsumer>(provider =>
    new KafkaConsumer(builder.Configuration["Kafka:BootstrapServers"]!, builder.Configuration["Kafka:Topic"]!, builder.Configuration["Kafka:GroupId"]!));

// Register Redis Cache Service
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:Configuration"];
    options.InstanceName = builder.Configuration["Redis:InstanceName"];
});

builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
