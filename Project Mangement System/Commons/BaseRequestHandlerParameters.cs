using DotNetCore.CAP;
using MediatR;

namespace Project_Mangement_System.Commons
{
    public class BaseRequestHandlerParameters
    {
        private readonly IMediator _mediator;
        private readonly ICapPublisher _capPublisher;

        public IMediator Mediator => _mediator;
        public ICapPublisher CapPublisher => _capPublisher;

        public BaseRequestHandlerParameters(IMediator mediator,ICapPublisher capPublisher)
        {
            _mediator = mediator;
            _capPublisher = capPublisher;
        }
    }
}
