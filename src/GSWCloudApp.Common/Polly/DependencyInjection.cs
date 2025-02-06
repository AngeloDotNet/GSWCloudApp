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
    public static AsyncRetryPolicy GetRetryPolicy(ILogger logger)
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(
            retryCount: 3, // Numero di tentativi
            sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(5, attempt)), // Intervallo esponenziale
            onRetry: (exc, timespan, attempt, context)
            => logger.LogWarning(exc, "Tentativo {Attempt} fallito. Riprovo tra {TimespanSeconds} secondi.", attempt, timespan.TotalSeconds));
    }
}
