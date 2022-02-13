using System.Security.Claims;

namespace BikeRental.Business.Interfaces
{
    public interface IJWTAuthService
    {
        string GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}