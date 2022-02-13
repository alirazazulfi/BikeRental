using BikeRental.DTO.DTO.Account;
using BikeRental.DTO.Response;

namespace BikeRental.Business.Interfaces
{
    public interface IUserAuthService
    {
        Task<DatabaseResponse> Login(LoginDTO dto);
        Task<DatabaseResponse> SignUp(SignUpDTO dto);
    }
}
