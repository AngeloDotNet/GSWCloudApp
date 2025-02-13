namespace GSWCloudApp.Common.Exceptions;

/// <summary>
/// Exception that is thrown when the API response is invalid.
/// </summary>
public class InvalidResponseApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseApiException"/> class.
    /// </summary>
    public InvalidResponseApiException()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseApiException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidResponseApiException(string? message) : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidResponseApiException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidResponseApiException(string? message, Exception? innerException) : base(message, innerException)
    { }
}