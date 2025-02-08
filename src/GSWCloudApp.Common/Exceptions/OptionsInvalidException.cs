namespace GSWCloudApp.Common.Exceptions;

/// <summary>
/// Exception that is thrown when the options provided are invalid.
/// </summary>
public class OptionsInvalidException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsInvalidException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public OptionsInvalidException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsInvalidException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public OptionsInvalidException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
