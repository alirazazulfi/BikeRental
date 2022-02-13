using BikeRental.Business.Interfaces;
using BikeRental.DTO.DTO;
using BikeRental.DTO.DTO.Search;
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
    public class BikeController : ControllerBase
    {
        #region ----- Properties -----

        private readonly IBikeService BikeService;
        private readonly IBikeRatingService BikeRatingService;
        #endregion

        #region ----- Constructor -----

        public BikeController(IBikeService _BikeService, IBikeRatingService _BikeRatingService)
        {
            BikeService = _BikeService;
            BikeRatingService = _BikeRatingService;
        }
        #endregion

        #region ----- Public Methods Manager ----- 

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(ApiResponse.OkResult(await BikeService.Get()));

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid Id) => Ok(ApiResponse.OkResult(await BikeService.GetById(Id)));

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BikeAddDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await BikeService.Add(dto)));
        }

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BikeUpdateDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await BikeService.Update(dto)));
        }

        [ServiceFilter(typeof(ManagerRoleAttribute))]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id) => Ok(ApiResponse.OkResult(await BikeService.Delete(Id)));

        #endregion

        #region ----- Public Methods User ----- 

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpPost("AvailableBikes")]
        public async Task<IActionResult> AvailableBikes([FromBody] AvailableBikeDTO dto) => Ok(ApiResponse.OkResult(await BikeService.AvailableBikes(dto)));

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] BikeSearchDTO dto) => Ok(ApiResponse.OkResult(await BikeService.Search(dto)));

        [ServiceFilter(typeof(UserRoleAttribute))]
        [HttpPost("Rating")] 
        public async Task<IActionResult> Rating([FromBody] BikeRatingAddDTO dto)
        {
            if (!ModelState.IsValid) return Ok(ApiResponse.ValidationErrorResponse(ModelState));
            return Ok(ApiResponse.OkResult(await BikeRatingService.Rate(dto)));
        }

        #endregion 
    }
}