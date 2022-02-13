using System.ComponentModel;

namespace BikeRental.DTO.Enums
{
    public enum DbReturnValue
    {
        [Description("Record created successfully")]
        CreateSuccess = 100,

        [Description("Record updated successfully")]
        UpdateSuccess = 101,

        [Description("Record does not exists")]
        NotExists = 102,

        [Description("Record deleted successfully")]
        DeleteSuccess = 103,

        [Description("Record exists in system")]
        RecordExists = 105,

        [Description("Record updation failed")]
        UpdationFailed = 106,

        [Description("Record creation failed")]
        CreationFailed = 107,

        [Description("Username already exists in system")]
        UsernameAlreadyExists = 108,

        [Description("Email address already exists in system")]
        EmailAlreadyExists = 109,

        [Description("Username or Password Incorrect")]
        UNameorPassinCorrect = 110,

        [Description("Your account is deactivated. Please Contact the Administrator")]
        UserInActive = 111,

        [Description("Invalid Perameter")]
        InvalidPerameter = 112
    }
}
