using FluentValidation;

namespace Project_Mangement_System.Features.ProjectManagement.Projects.AddProject
{
    public record AddProjectRequestViewModel(int id,string Name,string Description);
     
    public class AddProjectRequestViewModelValidator :AbstractValidator<AddProjectRequestViewModel>
    {
        public AddProjectRequestViewModelValidator() {
            RuleFor(x=>x.id).NotEmpty()
                .WithMessage("Project Not Found");
        }

    }


}
