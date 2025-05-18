namespace GSWCloudApp.Common.Mediator.Interfaces.Command;

/// <summary>
/// Represents a command with no return value.
/// </summary>
public interface ICommand
{ }

/// <summary>
/// Represents a command that returns a response of the specified type.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
public interface ICommand<TResponse>
{ }