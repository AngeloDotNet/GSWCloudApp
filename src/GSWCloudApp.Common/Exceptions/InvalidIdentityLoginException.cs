namespace GSWCloudApp.Common.Exceptions;

/// <summary>
/// Exception thrown when an identity login attempt is invalid.
/// </summary>
public class InvalidIdentityLoginException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentityLoginException"/> class.
    /// </summary>
    public InvalidIdentityLoginException()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentityLoginException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidIdentityLoginException(string? message) : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidIdentityLoginException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public InvalidIdentityLoginException(string? message, Exception? innerException) : base(message, innerException)
    { }
}
