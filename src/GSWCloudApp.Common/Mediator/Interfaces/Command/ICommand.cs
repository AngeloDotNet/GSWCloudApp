using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Command;

public interface ICommand<TResponse> : IRequest<TResponse>
{ }