namespace GSWCloudApp.Common.Exceptions;

public class OptionsInvalidException : Exception
{
    public OptionsInvalidException(string message) : base(message)
    {
    }

    public OptionsInvalidException(string message, Exception innerException) : base(message, innerException)
    {
    }
}