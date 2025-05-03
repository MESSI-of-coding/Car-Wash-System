using CarWash.DAL.Data;
using CarWash.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarWash.BL.Services;
using Microsoft.AspNetCore.Identity; // For IPasswordHasher and PasswordHasher
using CarWash.Domain.Models; // For User
using CarWash.API.Middleware;
using AutoMapper;
using CarWash.BL.Mappings;
using Microsoft.Extensions.Options;
using Stripe;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger services with authorization support
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite() // Enable geospatial support
    ));

// Add CarRepository to the DI container
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Register IPasswordHasher<User> service
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Register IJwtService in the DI container
builder.Services.AddScoped<IJwtService, JwtService>();

// Register IWashRequestRepository and WashRequestService in the DI container
builder.Services.AddScoped<IWashRequestRepository, WashRequestRepository>();
builder.Services.AddScoped<IWashRequestService, WashRequestService>();

// Register IUserRepository and UserRepository in the DI container
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Register NotificationService and ReminderService
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ReminderService>();

// Add services for controllers
builder.Services.AddControllers();

// Add Authorization services
builder.Services.AddAuthorization();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Validate Jwt:Key configuration
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured in appsettings.json (Jwt:Key). Ensure it is set.");
}
var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes)
        };
    });

// Bind Stripe settings from appsettings.json
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

// Configure Stripe API Key
var stripeSettings = builder.Configuration.GetSection("Stripe").Get<StripeSettings>();
if (stripeSettings != null)
{
    StripeConfiguration.ApiKey = stripeSettings.SecretKey;
}

// Add Hangfire services
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Hangfire server
builder.Services.AddHangfireServer();

// Register missing services (IEventBus, IEmailSender, ISmsSender) in the DI container
builder.Services.AddScoped<IEventBus, EventBus>(); // Replace EventBus with your actual implementation
builder.Services.AddScoped<IEmailSender, EmailSender>(); // Add email service
builder.Services.AddScoped<ISmsSender, SmsSender>(); // Add SMS service

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add ExceptionMiddleware to the pipeline
app.UseMiddleware<ExceptionMiddleware>();

// Add Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Use Hangfire Dashboard
app.UseHangfireDashboard();

// Register a recurring Hangfire job
RecurringJob.AddOrUpdate<ReminderService>("SendReminders", svc => svc.SendUpcomingRemindersAsync(), Cron.Daily);

// Schedule a Hangfire background job to run every X minutes
BackgroundJob.Schedule<AssignmentService>(svc => svc.AssignPendingRequestsAsync(), TimeSpan.FromMinutes(30));

// Add CarsController endpoints to Swagger
app.MapControllers();

app.Run();