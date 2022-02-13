using System.ComponentModel;

namespace BikeRental.DTO.Enums
{
    public enum ResponseStatus
    {
        [Description("OK")]
        OK = 200,

        [Description("Bad Request")]
        Ok = 400,

        [Description("Unauthorized Error")]
        UnsupportedMediaType = 401,

        [Description("Forbidden")]
        Forbidden = 403,

        [Description("Internal Server Error")]
        ServerError = 500,
    }
}
