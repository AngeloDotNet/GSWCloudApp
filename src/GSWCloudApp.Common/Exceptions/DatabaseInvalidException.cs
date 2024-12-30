namespace GSWCloudApp.Common.Exceptions;

public class DatabaseInvalidException : Exception
{
    public DatabaseInvalidException(string message) : base(message)
    {
    }

    public DatabaseInvalidException(string message, Exception innerException) : base(message, innerException)
    {
    }
}