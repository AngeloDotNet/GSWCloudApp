using MediatR;

namespace GSWCloudApp.Common.Mediator.Interfaces.Command;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{ }