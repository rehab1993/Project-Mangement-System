using Project_Mangement_System.Commons.Data;
using Project_Mangement_System.Helpers;

namespace Project_Mangement_System.Commons.Views
{
    public record EndPointResponse<T>(T Data, bool IsSuccess, string Message, ErrorCode StatusCode)
    {
        public static EndPointResponse<T> Success(T data, string massage = "")
        {
           return new EndPointResponse<T>(data,true,massage,ErrorCode.None);
        }
        public static EndPointResponse<T> Failure( ErrorCode ErrorCode)
        {
            return new EndPointResponse<T>(default,false,ErrorCode.GetDescription(),ErrorCode);
            
        }
        public static EndPointResponse<T> Failure(ErrorCode ErrorCode,string message)
        {
            return new EndPointResponse<T>(default, false, message, ErrorCode);

        }
    }

   

}
