using BikeRental.DTO.Enums;

namespace BikeRental.DTO.Response
{
    /// <summary>
    /// Operation response convey status of request - success/failure/validation error etc. without a result data collection.
    /// Mainly used on failure of requested operation or for a request which only have success message to display and no related result data object to be diplayed
    /// </summary>
    public class OperationResponse
    {
        public bool HasSucceeded { get; set; } 
        public string Message { get; set; }
        public string Exception { get; set; }
        public string StatusCode { get; set; }
        public object ReturnedObject { get; set; }

        public OperationResponse()
        {
            HasSucceeded = true;
            Message = "";
            Exception = ""; 
            StatusCode = ((int)ResponseStatus.OK).ToString();
        }
    }

    public class DatabaseResponse
    {
        public DbReturnValue DbReturnValue { get; set; }
        public bool HasSucceeded { get; set; }
        public int ResponseCode { get; set; }
        public object Results { get; set; }
    }
}
