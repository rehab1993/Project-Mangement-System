using DotNetCore.CAP;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Commons.Views;
using Project_Mangement_System.Features.ProjectManagement.Projects.AddProject;


namespace Project_Mangement_System.Commons
{
    [ApiController]
    [Route("[controller]/[action]")]
    //CustomAuthorizeFilter
    public class BaseEndPoint<TRequest,TResponse>:ControllerBase
    {
      
        protected IMediator _mediator;
        protected IValidator<TRequest> _validator;
        protected ICapPublisher _capPublisher;
      

        public BaseEndPoint(BaseEndPointParameters<TRequest> parameters)
        {
            _mediator = parameters.Mediator;
            _validator = parameters.Validator;
            _capPublisher = parameters.CapPublisher;
            
        }
        protected EndPointResponse<TResponse> ValidateRequest(TRequest request) {

            var ValidationResult = _validator.Validate(request);
            if (!ValidationResult.IsValid)
            {
                var ValidationErrors = string.Join(" , ", ValidationResult.Errors.Select(x => x.ErrorMessage));
                return EndPointResponse<TResponse>.Failure(ErrorCode.NotFound, ValidationErrors);


            }
            return EndPointResponse<TResponse>.Success(default);

        }
    }
}
