using BikeRental.Business.Interfaces;
using BikeRental.Common;
using BikeRental.DTO.DTO;
using BikeRental.DTO.DTO.Search;
using BikeRental.DTO.Enums;
using BikeRental.DTO.Response;
using BikeRental.Entities;
using BikeRental.Entities.DBEtities;
using BikeRental.Mapper;
using BikeRental.Mapper.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BikeRental.Business.Services
{
    public class BikeService : BaseService, IBikeService
    {
        #region ----- Properties -----
        private readonly IEfRepository<Bike> BikeRepo;
        private readonly IEfRepository<Reservation> ReservationRepo;
        #endregion

        #region ----- Constructor -----

        public BikeService(IEfRepository<Bike> _BikeRepo, IEfRepository<Reservation> _ReservationRepo, IHttpContextAccessor _contextAccessor) : base(_contextAccessor)
        {
            BikeRepo = _BikeRepo;
            ReservationRepo = _ReservationRepo;
        }

        #endregion

        #region ----- Public Methods ----- 

        public async Task<DatabaseResponse> Get()
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => !i.IsDeleted,
                Includes = { x => x.BikeRatings },
                OrderBy = x => x.Name,
                AsNoTracking = true
            };

            var data = await BikeRepo.GetDataAsync(exp);

            List<BikeDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new BikeMap().GetDTOResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        }

        public async Task<DatabaseResponse> GetById(Guid Id)
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.Id == Id && !i.IsDeleted,
                Includes = { x => x.BikeRatings },
                AsNoTracking = true
            };
            var data = await BikeRepo.GetFirstAsync(exp);

            if (data != null)
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.RecordExists,
                    DbReturnValue = DbReturnValue.RecordExists,
                    HasSucceeded = true,
                    Results = new BikeMap().GetDTOResponse(data)
                };
            }
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.NotExists,
                DbReturnValue = DbReturnValue.NotExists,
                HasSucceeded = false
            };
        }

        public async Task<DatabaseResponse> Add(BikeAddDTO dto)
        {
            Bike obj = new BikeMap().Add(dto);

            await BikeRepo.AddAsync(obj);
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.CreateSuccess,
                DbReturnValue = DbReturnValue.CreateSuccess,
                HasSucceeded = true
            };
        }

        public async Task<DatabaseResponse> Update(BikeUpdateDTO dto)
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.Id == dto.Id && !i.IsDeleted
            };
            var Bike = await BikeRepo.GetFirstAsync(exp);

            if (Bike != null)
            {
                new BikeMap().Update(Bike, dto);
                await BikeRepo.UpdateAsync(Bike);
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

        public async Task<DatabaseResponse> Delete(Guid Id)
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.Id == Id && !i.IsDeleted
            };
            var oldData = await BikeRepo.GetFirstAsync(exp);
            if (oldData != null)
            {
                new MarkDelete<Bike>().Mark(oldData);

                await BikeRepo.UpdateAsync(oldData);
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.DeleteSuccess,
                    DbReturnValue = DbReturnValue.DeleteSuccess,
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

        public async Task<DatabaseResponse> BikeReport()
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => !i.IsDeleted && i.Reservations.Count() > 0,
                Includes = { x => x.Reservations },
                ThenIncludes = { "Reservations.User" },
                AsNoTracking = true
            };

            var data = await BikeRepo.GetDataAsync(exp);

            List<BikeReportDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new BikeMap().GetBikeReportResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        }

        public async Task<DatabaseResponse> Search(BikeSearchDTO dto)
        {
            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.IsAvailable && !i.IsDeleted &&

                                (dto.Name.IsNullEmpty() || EF.Functions.Like(i.Name, "%" + dto.Name + "%")) &&

                                (dto.Model.IsNullEmpty() || EF.Functions.Like(i.Model, "%" + dto.Model + "%")) &&

                                (dto.Location.IsNullEmpty() || EF.Functions.Like(i.Location, "%" + dto.Location + "%")) &&

                                (dto.EndRate == 0 || (i.PerDayRate >= dto.StartRate && i.PerDayRate <= dto.EndRate)),

                OrderBy = x => x.Name,
                Includes = { x => x.BikeRatings },
                AsNoTracking = true
            };

            var data = await BikeRepo.GetDataAsync(exp);

            List<BikeDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new BikeMap().GetDTOResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        }

        public async Task<DatabaseResponse> AvailableBikes(AvailableBikeDTO dto)
        {
            var reservations = await GetReservation(dto);

            var exp = new QueryExpression<Bike>
            {
                Predicate = i => i.IsAvailable && !i.IsDeleted && !reservations.Select(x => x.BikeId).Contains(i.Id),
                OrderBy = x => x.Name,
                Includes = { x => x.BikeRatings },
                AsNoTracking = true
            };

            var bikes = await BikeRepo.GetDataAsync(exp); 

            List<BikeDTO> returnDTO = new();

            foreach (var item in bikes) { returnDTO.Add(new BikeMap().GetDTOResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        } 

        private async Task<IEnumerable<Reservation>> GetReservation(AvailableBikeDTO dto)
        {
            var exp = new QueryExpression<Reservation>
            {
                Predicate = i => !i.IsDeleted && (i.StartDate >= dto.StartDate && i.EndDate <= dto.EndDate),
                AsNoTracking = true,
                OrderByDesc = i => i.CreatedDate
            };

            return await ReservationRepo.GetDataAsync(exp);
        }

        #endregion
    }
}