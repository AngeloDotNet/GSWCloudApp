namespace GSWCloudApp.Common.Exceptions;

/// <summary>
/// Exception that is thrown when an endpoint is unreachable.
/// </summary>
public class EndpointUnreachableException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointUnreachableException"/> class.
    /// </summary>
    public EndpointUnreachableException()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointUnreachableException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public EndpointUnreachableException(string? message) : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EndpointUnreachableException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public EndpointUnreachableException(string? message, Exception? innerException) : base(message, innerException)
    { }
}
