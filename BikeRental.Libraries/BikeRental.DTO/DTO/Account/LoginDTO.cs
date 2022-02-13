using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}