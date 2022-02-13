using BikeRental.Business.Interfaces;
using BikeRental.DTO.DTO;
using BikeRental.DTO.Helpers;
using BikeRental.Filters.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BikeRental.Controllers
{
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    [Route("api/[controller]")]
    [Authorize]
    [ServiceFilter(typeof(ManagerRoleAttribute))]
    public class UserController : ControllerBase
    {
        #region ----- Properties -----

        private readonly IUserManagementService UserService;
        #endregion

        #region ----- Constructor -----

        public UserController(IUserManagementService _UserService) => UserService = _UserService;
        #endregion

        #region ----- Public Methods ----- 

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(ApiResponse.OkResult(await UserService.Get()));

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid Id) => Ok(ApiResponse.OkResult(await UserService.GetById(Id)));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserAddDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await UserService.Add(dto)));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await UserService.Update(dto)));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id) => Ok(ApiResponse.OkResult(await UserService.Delete(Id)));
        
        #endregion
    }
}