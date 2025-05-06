using DotNetCore.CAP;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Commons.Views;
using Project_Mangement_System.Features.ProjectManagement.Projects.AddProject;


namespace Project_Mangement_System.Commons
{
    // [ApiController]
    // [Route("[controller]/[action]")]
    // [Route("api/[controller]")]
    //CustomAuthorizeFilter
    //public class BaseEndPoint<TRequest,TResponse>:ControllerBase
    //{

    //protected IMediator _mediator;
    //protected IValidator<TRequest> _validator;
    //protected ICapPublisher _capPublisher;
    //protected readonly BaseEndPointParameters<TRequest> _Parameters;


    //public BaseEndPoint(BaseEndPointParameters<TRequest> parameters)
    //{
    // _Parameters = parameters;
    //_mediator = parameters.Mediator;
    //_validator = parameters.Validator;
    //_capPublisher = parameters.CapPublisher;

    //    }
    //    protected EndPointResponse<TResponse> ValidateRequest(TRequest request) {

    //        var ValidationResult = _Parameters.Validator.Validate(request);
    //        if (!ValidationResult.IsValid)
    //        {
    //            var ValidationErrors = string.Join(" , ", ValidationResult.Errors.Select(x => x.ErrorMessage));
    //            return EndPointResponse<TResponse>.Failure(ErrorCode.NotFound, ValidationErrors);


    //        }
    //        return EndPointResponse<TResponse>.Success(default);

    //    }
    //}



    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseEndPoint<TRequest, TResponse> : ControllerBase
    {
        protected readonly BaseEndPointParameters<TRequest> _Parameters;

        protected BaseEndPoint(BaseEndPointParameters<TRequest> parameters)
        {
            _Parameters = parameters;
        }

        protected EndPointResponse<TResponse> ValidateRequest(TRequest request)
        {

            var ValidationResult = _Parameters.Validator.Validate(request);
            if (!ValidationResult.IsValid)
            {
                var ValidationErrors = string.Join(" , ", ValidationResult.Errors.Select(x => x.ErrorMessage));
                return EndPointResponse<TResponse>.Failure(ErrorCode.NotFound, ValidationErrors);


            }
            return EndPointResponse<TResponse>.Success(default);
         
        }

        protected async Task<EndPointResponse<TResponse>> SendRequestAsync(IRequest<EndPointResponse<TResponse>> request)
        {
            try
            {
                return await _Parameters.Mediator.Send(request);
            }
            catch (Exception ex)
            {
               // Parameters.Logger.LogError(ex, "Failed to process request.");
                return EndPointResponse<TResponse>.Failure(ErrorCode.InternalServerError, ex.Message);
            }
        }

        protected Task PublishMessageAsync<T>(string topic, T message)
        {
            return _Parameters.CapPublisher.PublishAsync(topic, message);
        }
    }

}
