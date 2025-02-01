using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Query;

public interface IQuery<TResponse> : IRequest<TResponse>
{ }