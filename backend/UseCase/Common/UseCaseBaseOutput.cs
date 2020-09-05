using Microsoft.AspNetCore.Http;

namespace Backend.API.UseCase.Common{

    public class UseCaseBaseOutput<T>
    {
        public int httpStatus {get;set;}
        public T value {get;set;}
        public UseCaseBaseOutput(T value){
            this.value = value;
            this.httpStatus = StatusCodes.Status200OK;
        }

        public UseCaseBaseOutput(int statusCode)
        {
            this.httpStatus = statusCode;
        }

        public UseCaseBaseOutput(T value,int statusCode){
            this.value = value;
            this.httpStatus = statusCode;
        }

    }
}