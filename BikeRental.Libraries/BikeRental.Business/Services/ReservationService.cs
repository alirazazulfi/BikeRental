using BikeRental.Business.Interfaces;
using BikeRental.Common;
using BikeRental.DTO.DTO;
using BikeRental.DTO.Enums;
using BikeRental.DTO.Response;
using BikeRental.Entities;
using BikeRental.Entities.DBEtities;
using BikeRental.Mapper.Mappings;
using Microsoft.AspNetCore.Http;

namespace BikeRental.Business.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        #region ----- Properties -----
        private readonly IEfRepository<Bike> BikeRepo;
        private readonly IEfRepository<Reservation> ReservationRepo;
        #endregion

        #region ----- Constructor -----

        public ReservationService(IEfRepository<Reservation> _ReservationRepo, IEfRepository<Bike> _BikeRepo, IHttpContextAccessor _contextAccessor) : base(_contextAccessor)
        {
            ReservationRepo = _ReservationRepo;
            BikeRepo = _BikeRepo;
        }

        #endregion

        #region ----- Public Methods ----- 
         
        public async Task<DatabaseResponse> GetByUserId()
        {
            var exp = new QueryExpression<Reservation>
            {
                Predicate = i => i.UserId == new Guid(CLAIMS.UserId) && !i.IsDeleted,
                Includes = { x => x.User, x => x.Bike.BikeRatings },
                AsNoTracking = true,
                OrderByDesc = i => i.CreatedDate
            };

            var data = await ReservationRepo.GetDataAsync(exp);

            List<ReservationDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new ReservationMap().GetDTOResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        }
         
        public async Task<DatabaseResponse> Add(ReservationAddDTO dto)
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.Id == dto.BikeId && i.IsAvailable && !i.IsDeleted,
                AsNoTracking = true
            };
            var bike = await BikeRepo.GetFirstAsync(exp);

            if (bike != null)
            {
                Reservation obj = new ReservationMap().Add(dto, bike.PerDayRate, new Guid(CLAIMS.UserId));

                await ReservationRepo.AddAsync(obj);
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.CreateSuccess,
                    DbReturnValue = DbReturnValue.CreateSuccess,
                    HasSucceeded = true
                };
            }
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.NotExists,
                DbReturnValue = DbReturnValue.NotExists,
                HasSucceeded = false
            };
        }

        public async Task<DatabaseResponse> Cancel(ReservationCancelDTO dto)
        {
            var exp = new QueryExpression<Reservation>
            {
                Predicate = i => i.Id == dto.Id && i.UserId == new Guid(CLAIMS.UserId) && !i.IsDeleted
            };
            var Reservation = await ReservationRepo.GetFirstAsync(exp);

            if (Reservation != null)
            {
                new ReservationMap().Cancel(Reservation, dto);
                await ReservationRepo.UpdateAsync(Reservation);
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.UpdateSuccess,
                    DbReturnValue = DbReturnValue.UpdateSuccess,
                    HasSucceeded = true
                };
            }
            else
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.NotExists,
                    DbReturnValue = DbReturnValue.NotExists,
                    HasSucceeded = false
                };
            }
        }
         
        #endregion
    }
}