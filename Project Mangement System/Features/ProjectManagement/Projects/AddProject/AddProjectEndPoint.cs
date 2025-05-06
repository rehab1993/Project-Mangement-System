using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_Mangement_System.Commons;
using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Commons.Views;

namespace Project_Mangement_System.Features.ProjectManagement.Projects.AddProject
{


   
    public class AddProjectEndPoint: BaseEndPoint<AddProjectRequestViewModel,AddProjectResponseViewModel>
    {
       
        private readonly BaseEndPointParameters<AddProjectRequestViewModel> _parameters;

        public AddProjectEndPoint(BaseEndPointParameters<AddProjectRequestViewModel> parameters)
        :base(parameters){
           
            _parameters = parameters;
        }

        [HttpPost]
        public EndPointResponse<AddProjectResponseViewModel> AddProject(AddProjectRequestViewModel viewModel)
        {
            var ValidationResult = ValidateRequest(viewModel);
            if (!ValidationResult.IsSuccess) {
                return ValidationResult;
            }

        
            return EndPointResponse<AddProjectResponseViewModel>.Success(default);

        }

        [HttpPost]
        public void PublishMessage()
        {
            _parameters.CapPublisher.Publish("cap test", new { Id = 111 , Name = "ali" });
           
        }
        
        
    }
}
