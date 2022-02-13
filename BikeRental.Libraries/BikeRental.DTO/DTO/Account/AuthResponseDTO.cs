namespace BikeRental.DTO.DTO.Account
{
    public class AuthResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int UserRoleId { get; set; }
        public string UserRole { get; set; }
        public string JWToken { get; set; } = string.Empty;
    }
}