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
    public class ReservationController : ControllerBase
    {
        #region ----- Properties -----

        private readonly IReservationService ReservationService;
        private readonly IUserManagementService UserService;
        private readonly IBikeService BikeService;
        #endregion

        #region ----- Constructor -----

        public ReservationController(IReservationService _ReservationService, IUserManagementService _UserService, IBikeService _BikeService)
        {
            ReservationService = _ReservationService;
            UserService = _UserService;
            BikeService = _BikeService;
        }
        #endregion

        #region ----- Public Methods Manager ----- 

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpGet("UserReport")]
        public async Task<IActionResult> UserReport() => Ok(ApiResponse.OkResult(await UserService.UserReport()));

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpGet("BikeReport")]
        public async Task<IActionResult> BikeReport() => Ok(ApiResponse.OkResult(await BikeService.BikeReport()));

        #endregion

        #region ----- Public Methods User----- 

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpGet("GetForUser")]
        public async Task<IActionResult> GetForUser() => Ok(ApiResponse.OkResult(await ReservationService.GetByUserId()));

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpPost("ReserveBike")]
        public async Task<IActionResult> ReserveBike([FromBody] ReservationAddDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await ReservationService.Add(dto)));
        }

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpPost("Cancel")]
        public async Task<IActionResult> Cancel([FromBody] ReservationCancelDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await ReservationService.Cancel(dto)));
        }

        #endregion 
    }
}