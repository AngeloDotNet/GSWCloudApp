﻿namespace GSWCloudApp.Common.Exceptions;

/// <summary>
/// Exception that is thrown when an invalid refresh token is encountered.
/// </summary>
public class InvalidRefreshTokenException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidRefreshTokenException"/> class.
    /// </summary>
    public InvalidRefreshTokenException()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidRefreshTokenException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidRefreshTokenException(string? message) : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidRefreshTokenException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public InvalidRefreshTokenException(string? message, Exception? innerException) : base(message, innerException)
    { }
}
