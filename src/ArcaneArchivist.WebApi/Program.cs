using ArcaneArchivist.WebApi;
using ArcaneArchivist.WebApi.Common;
using Carter;

var builder = WebApplication.CreateBuilder(args);

var configuration = AppSettings.Configuration();

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

app.MapCarter();
app.Run();