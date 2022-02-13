using BikeRental.Business.Interfaces;
using BikeRental.DTO.DTO.Account;
using BikeRental.DTO.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Controllers
{
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    [Route("api/")]
    [AllowAnonymous]
    public class UserAuthController : ControllerBase
    { 
        private readonly IUserAuthService UserAuth;

        public UserAuthController(IUserAuthService _UserAuth) => UserAuth = _UserAuth;

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await UserAuth.Login(dto)));
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await UserAuth.SignUp(dto)));
        }
    }
}