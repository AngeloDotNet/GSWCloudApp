using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Query;

/// <summary>
/// Defines a handler for a query that returns a response.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{ }
