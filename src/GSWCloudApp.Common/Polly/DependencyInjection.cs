using GSWCloudApp.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace GSWCloudApp.Common.Polly;

public static class DependencyInjection
{
    /// <summary>
    /// Creates an asynchronous retry policy with exponential backoff.
    /// </summary>
    /// <param name="logger">The logger to use for logging retry attempts.</param>
    /// <returns>An asynchronous retry policy.</returns>
    public static AsyncRetryPolicy GetRetryPolicy(this IConfiguration configuration, ILogger logger)
    {
        var options = configuration.GetSection("PollyPolicyOptions").Get<PollyPolicyOptions>() ?? new();

        return Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryCount: options.RetryCount, sleepDurationProvider: attempt
                => TimeSpan.FromSeconds(Math.Pow(options.SleepDuration, attempt)),
                onRetry: (exc, timespan, attempt, context)
                    => logger.LogWarning(exc, "Tentativo {Attempt} fallito. Riprovo tra {TimespanSeconds} secondi.", attempt, timespan.TotalSeconds));
    }
}
