using ArcaneArchivist.WebApi.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MagicCardDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        options =>
        {
            options.EnableRetryOnFailure(2,
                TimeSpan.FromSeconds(3),
                new List<int>());
        });
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();