using BikeRental.DTO.Enums;
using BikeRental.DTO.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BikeRental.DTO.Helpers
{
    public static class ApiResponse
    {
        public static OperationResponse ExceptionResponse(Exception ex)
        {
            string ExceptionMessage = ex.Message;
            if (ex.InnerException != null) ExceptionMessage += " || " + ex.InnerException;
            return new OperationResponse
            {
                HasSucceeded = false,
                Message = StatusMessages.ServerError,
                //TODO: Exception message will be displayed to super admin or in dev mode only
                Exception = ExceptionMessage,
                StatusCode = ((int)ResponseStatus.ServerError).ToString()
            };
        }

        public static OperationResponse ValidationErrorResponse(ModelStateDictionary ModelState)
        {
            return new OperationResponse
            {
                HasSucceeded = false, 
                StatusCode = ((int)ResponseStatus.Ok).ToString(),
                Message = string.Join("; ", ModelState.Values
                                                     .SelectMany(x => x.Errors)
                                                     .Select(x => x.ErrorMessage))
            };
        }

        public static OperationResponse MissingPerameter(DbReturnValue dbReturnValue)
        {
            return new OperationResponse
            {
                HasSucceeded = false, 
                StatusCode = ((int)ResponseStatus.Ok).ToString(),
                Message = dbReturnValue.GetDescription()
            };
        }

        public static OperationResponse OkResult(DatabaseResponse response)
        {
            return new OperationResponse
            {
                HasSucceeded = response.HasSucceeded, 
                Message = response.DbReturnValue.GetDescription(),
                ReturnedObject = response.Results
            };
        }
    }
}
