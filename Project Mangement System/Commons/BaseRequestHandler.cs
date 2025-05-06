using DotNetCore.CAP;
using MediatR;

namespace Project_Mangement_System.Commons
{
    public abstract class BaseRequestHandler<TRequest,TResponse> :IRequestHandler<TRequest,TResponse> where TRequest : IRequest<TResponse>
    {
        protected IMediator _mediator;
        protected ICapPublisher _capPublisher;
        public BaseRequestHandler(BaseRequestHandlerParameters parameters) {
            _mediator = parameters.Mediator;
            _capPublisher = parameters.CapPublisher;

        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
       
    }
}
