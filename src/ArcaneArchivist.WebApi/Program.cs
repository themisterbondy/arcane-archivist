using ArcaneArchivist.WebApi;
using ArcaneArchivist.WebApi.Common;
using ArcaneArchivist.WebApi.Middleware;
using Carter;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = AppSettings.Configuration();

builder.Host.UseSerilog((_, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(configuration));

builder.Services
    .AddWebApi(configuration)
    .AddMediat()
    .AddSwagger();

var app = builder.Build();

app.UseHealthChecksConfiguration();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseMiddleware<RequestContextLoggingMiddleware>();

app.MapCarter();
app.Run();