using Azure.Core;
using DotNetCore.CAP;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project_Mangement_System.Commons;
using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Commons.Views;
using Project_Mangement_System.Features.ProjectManagement.Projects.AddProject.Commands;

namespace Project_Mangement_System.Features.ProjectManagement.Projects.AddProject
{

   
    public class AddProjectEndPoint : BaseEndPoint<AddProjectRequestViewModel, AddProjectResponseViewModel>
    {
        private readonly BaseEndPointParameters<AddProjectRequestViewModel> _parameters;
        public AddProjectEndPoint(BaseEndPointParameters<AddProjectRequestViewModel> parameters)
            : base(parameters) {
            _parameters = parameters;
        }

      

        [HttpPost]
        public async Task<EndPointResponse<AddProjectResponseViewModel>> AddProject( AddProjectRequestViewModel viewModel)
        {
            var validation = ValidateRequest(viewModel);
            if (!validation.IsSuccess)
                return validation;

            return EndPointResponse<AddProjectResponseViewModel>.Success(default);


            //var command = new AddProjectCommand(viewModel);
            //return await SendRequestAsync(command);
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishTestMessage()
        {
            // _Parameters.CapPublisher.Publish("cap.test", new { Id = 123, Name = "Project" });
            await PublishMessageAsync("capTest", new { Id = 123, Name = "Project" });
            return Ok("Message published.");
        }
    }

}
