using System.ComponentModel;

namespace Project_Mangement_System.Commons.Data
{
    public enum ErrorCode
    {

        None = 0,

        [Description("A general internal server error occurred.")]
        InternalServerError = 500,

        [Description("The requested item was not found.")]
        NotFound = 404,

        [Description("The request was invalid.")]
        BadRequest = 400,

        [Description("Unauthorized access.")]
        Unauthorized = 401,
       
        [Description("UnKnownError")]
        AlreadyExist = 100,


        FailerUpdated = 101,
        FailerDelete = 102,
        Success = 200,
        ServerError = 501,
        BusinessRuleViolation = 700,

    }
}
