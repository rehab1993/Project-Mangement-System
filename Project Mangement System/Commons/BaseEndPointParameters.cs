using DotNetCore.CAP;
using FluentValidation;
using MediatR;

namespace Project_Mangement_System.Commons
{
    public class BaseEndPointParameters<TRequest>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<TRequest> _validator;
        private readonly ICapPublisher _capPublisher;

        public IMediator Mediator => _mediator;
        public IValidator<TRequest> Validator => _validator;
        public ICapPublisher CapPublisher => _capPublisher;

        public BaseEndPointParameters(IMediator mediator,IValidator<TRequest> validator,ICapPublisher capPublisher) {
            _mediator = mediator;
            _validator = validator;
            _capPublisher = capPublisher;
        }

    }
}
