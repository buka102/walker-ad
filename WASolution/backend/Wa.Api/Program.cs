using FluentValidation;
using Wa.Application.Services;
using Wa.Application.Interfaces;
using Serilog;
using Wa.Api.Filters;
using Wa.Api.Middlewares;
using Wa.Application.Entities;
using Wa.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

//  Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// Register dependencies
builder.Services.AddScoped<PartnersApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<ILeadService, LeadService>();
builder.Services.AddScoped<INotifierService, NotifierService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(LeadCustomMappingProfile));

builder.Services.AddSingleton<IValidator<CreateLeadDto>, CreateLeadDtoValidator>();

var app = builder.Build();

// Register custom logging middleware
app.UseMiddleware<CustomLoggingMiddleware>();
app.UseMiddleware<CustomExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();