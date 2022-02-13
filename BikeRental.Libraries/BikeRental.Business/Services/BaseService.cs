using BikeRental.DTO.DTO.Account;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BikeRental.Business.Services
{
    /// <summary>
    /// Base Service Contains All Common Functions and Properties For All Services.
    /// </summary>
    public class BaseService
    {
        #region ----- Properties -----

        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Current User's Claims. <para>Defined in BaseService</para>
        /// </summary>

        public readonly ClaimsDTO CLAIMS;

        #endregion

        #region ----- Constructor -----

        public BaseService(IHttpContextAccessor _contextAccessor)
        {
            contextAccessor = _contextAccessor;
            var context = contextAccessor.HttpContext;
            CLAIMS = new();
            if (context.User != null)
            {
                CLAIMS.UserId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                CLAIMS.UserName = context.User.FindFirst(ClaimTypes.Name)?.Value;
                CLAIMS.UserRole = context.User.FindFirst(ClaimTypes.Role)?.Value;
            }
        }

        #endregion

    }
}
