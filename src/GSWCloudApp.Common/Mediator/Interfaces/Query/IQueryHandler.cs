using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Query;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{ }