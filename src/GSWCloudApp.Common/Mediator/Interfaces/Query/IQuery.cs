using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Query;

/// <summary>
/// Represents a query with a response type that implements the MediatR IRequest interface.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQuery<TResponse> : IRequest<TResponse>
{ }
