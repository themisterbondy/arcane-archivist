using System.Reflection;
using System.Text.Json.Serialization;
using ArcaneArchivist.WebApi.Common;
using ArcaneArchivist.WebApi.Common.Behavior;
using ArcaneArchivist.WebApi.Common.Messaging;
using ArcaneArchivist.WebApi.Jobs;
using ArcaneArchivist.WebApi.Messaging.MegicCard;
using ArcaneArchivist.WebApi.Persistence;
using ArcaneArchivist.WebApi.Services;
using Azure.Storage.Queues;
using Carter;
using Carter.OpenApi;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ArcaneArchivist.WebApi;

public static class DependencyInjection
{
    private static readonly Assembly _Assembly = typeof(Program).Assembly;

    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MagicCardDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SQLConnection"),
                options =>
                {
                    options.EnableRetryOnFailure(2,
                        TimeSpan.FromSeconds(3),
                        new List<int>());
                });
        });

        services.AddExceptionHandler<GlobalExeptionHandler>();
        services.AddProblemDetails();
        services.AddUseHealthChecksConfiguration(configuration);
        services.AddValidatorsFromAssembly(_Assembly);
        services.AddCarter();

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });


        var storageConnectionString = configuration.GetValue<string>("AzureStorageSettings:ConnectionString");
        services.AddSingleton(x => new QueueServiceClient(storageConnectionString));

        services.AddSingleton<IQueue, AzureQueue>();

        services.AddScoped<IMegicCardCreatedQueue, MegicCardCreatedQueue>();
        services.AddSingleton<IConsumerQueue, MegicCardCreatedQueue>();
        services.AddSingleton<IPublisherQueue, MegicCardCreatedQueue>();

        services.AddScoped<ITesteQueue, TesteQueue>();
        services.AddSingleton<IConsumerQueue, TesteQueue>();
        services.AddSingleton<IPublisherQueue, TesteQueue>();

        services.AddHostedService<MagicCardCreatedJob>();
        services.AddHostedService<TesteJob>();

        return services;
    }

    public static IServiceCollection AddMediat(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(_Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "Magic Cards API",
                Version = "v1",
                Title = "Magic Cards API"
            });
            options.DocInclusionPredicate((s, description) =>
                description.ActionDescriptor.EndpointMetadata.OfType<IIncludeOpenApi>().Any());
        });
        return services;
    }
}
