using ArcaneArchivist.SharedKernel;
using MediatR;
using Serilog.Context;

namespace ArcaneArchivist.WebApi.Common.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestname = typeof(TRequest).Name;

        logger.LogInformation(
            "Starting request {@RequestName}", requestname);

        var result = await next();

        if (result.IsFailure)
        {
            LogContext.PushProperty("Error", result.Error, true);
            LogContext.PushProperty("Errors", result.Errors, true);
            logger.LogError(
                "Completed request {@RequestName} with error", requestname);
        }

        logger.LogInformation(
            "Completed request {@RequestName}", requestname);

        return result;
    }
}