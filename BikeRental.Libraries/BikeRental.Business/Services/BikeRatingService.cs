using BikeRental.Business.Interfaces;
using BikeRental.Common;
using BikeRental.DTO.DTO;
using BikeRental.DTO.Enums;
using BikeRental.DTO.Response;
using BikeRental.Entities;
using BikeRental.Entities.DBEtities;
using BikeRental.Mapper;
using BikeRental.Mapper.Mappings;
using Microsoft.AspNetCore.Http;

namespace BikeRental.Business.Services
{
    public class BikeRatingService : BaseService, IBikeRatingService
    {
        #region ----- Properties -----
        private readonly IEfRepository<BikeRating> BikeRatingRepo;
        #endregion

        #region ----- Constructor -----

        public BikeRatingService(IEfRepository<BikeRating> _BikeRatingRepo, IHttpContextAccessor _contextAccessor) : base(_contextAccessor) => BikeRatingRepo = _BikeRatingRepo;

        #endregion

        #region ----- Public Methods ----- 

        public async Task<DatabaseResponse> GetByBikeId(Guid BikeId)
        {
            var exp = new QueryExpression<BikeRating>
            {
                Predicate = i => i.BikeId == BikeId && !i.IsDeleted,
                Includes = { x => x.Bike, x => x.User },
                AsNoTracking = true
            };
            var data = await BikeRatingRepo.GetFirstAsync(exp);

            if (data != null)
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.RecordExists,
                    DbReturnValue = DbReturnValue.RecordExists,
                    HasSucceeded = true,
                    Results = new BikeRatingMap().GetDTOResponse(data)
                };
            }
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.NotExists,
                DbReturnValue = DbReturnValue.NotExists,
                HasSucceeded = false
            };
        }

        public async Task<DatabaseResponse> GetByUserId(Guid UserId)
        {
            var exp = new QueryExpression<BikeRating>
            {
                Predicate = i => i.UserId == UserId && !i.IsDeleted,
                Includes = { x => x.Bike, x => x.User },
                AsNoTracking = true
            };
            var data = await BikeRatingRepo.GetFirstAsync(exp);

            if (data != null)
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.RecordExists,
                    DbReturnValue = DbReturnValue.RecordExists,
                    HasSucceeded = true,
                    Results = new BikeRatingMap().GetDTOResponse(data)
                };
            }
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.NotExists,
                DbReturnValue = DbReturnValue.NotExists,
                HasSucceeded = false
            };
        }

        public async Task<DatabaseResponse> Rate(BikeRatingAddDTO dto)
        {
            BikeRating obj = new BikeRatingMap().Add(dto, new Guid(CLAIMS.UserId));

            await BikeRatingRepo.AddAsync(obj);
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.CreateSuccess,
                DbReturnValue = DbReturnValue.CreateSuccess,
                HasSucceeded = true
            };
        }
        #endregion
    }
}
