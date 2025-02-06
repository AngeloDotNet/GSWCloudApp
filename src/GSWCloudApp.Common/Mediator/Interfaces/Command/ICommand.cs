using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Command;

/// <summary>
/// Represents a command with a response type that implements the MediatR IRequest interface.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface ICommand<TResponse> : IRequest<TResponse>
{ }
