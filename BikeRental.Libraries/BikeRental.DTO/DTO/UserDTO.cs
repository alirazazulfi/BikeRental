using System.ComponentModel.DataAnnotations;

namespace BikeRental.DTO.DTO
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
    }

    public class UserAddDTO
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name must be between 3 to 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name must be between 3 to 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters are not allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [StringLength(maximumLength: 40, MinimumLength = 6, ErrorMessage = "Username must be between 6 to 40 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*_)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please select user role")]
        [Range(1, 2, ErrorMessage = "User Role can be 1. Manager or 2. User")]
        public int UserRole { get; set; }

        public bool Active { get; set; }
    }

    public class UserUpdateDTO : UserAddDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}