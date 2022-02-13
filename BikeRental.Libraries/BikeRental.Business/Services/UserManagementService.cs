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
    public class UserManagementService : BaseService, IUserManagementService
    {
        #region ----- Properties -----
        private readonly IEfRepository<User> UserRepo;
        #endregion

        #region ----- Constructor -----

        public UserManagementService(IEfRepository<User> _UserRepo, IHttpContextAccessor _contextAccessor) : base(_contextAccessor) => UserRepo = _UserRepo;

        #endregion

        #region ----- Public Methods ----- 

        public async Task<DatabaseResponse> Get()
        {
            var exp = new QueryExpression<User>
            {
                Predicate = i => !i.IsDeleted,
                OrderBy = x => x.FirstName,
                AsNoTracking = true
            };

            var data = await UserRepo.GetDataAsync(exp);

            List<UserDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new UserMap().GetDTOResponse(item)); }

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
            var exp = new QueryExpression<User>
            {
                Predicate = i => i.Id == Id && !i.IsDeleted,
                AsNoTracking = true
            };
            var data = await UserRepo.GetFirstAsync(exp);

            if (data != null)
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.RecordExists,
                    DbReturnValue = DbReturnValue.RecordExists,
                    HasSucceeded = true,
                    Results = new UserMap().GetDTOResponse(data)
                };
            }
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.NotExists,
                DbReturnValue = DbReturnValue.NotExists,
                HasSucceeded = false
            };
        }

        public async Task<DatabaseResponse> Add(UserAddDTO dto)
        {
            if (await UserRepo.GetAnyAsync(x => x.Username.Trim().ToLower() == dto.Username.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.UsernameAlreadyExists,
                    DbReturnValue = DbReturnValue.UsernameAlreadyExists,
                    HasSucceeded = false
                };
            }

            else if (await UserRepo.GetAnyAsync(x => x.Email.Trim().ToLower() == dto.Email.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.EmailAlreadyExists,
                    DbReturnValue = DbReturnValue.EmailAlreadyExists,
                    HasSucceeded = false
                };
            }

            User obj = new UserMap().Add(dto);

            await UserRepo.AddAsync(obj);
            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.CreateSuccess,
                DbReturnValue = DbReturnValue.CreateSuccess,
                HasSucceeded = true
            };
        }

        public async Task<DatabaseResponse> Update(UserUpdateDTO dto)
        {
            if (await UserRepo.GetAnyAsync(x => x.Id != dto.Id && x.Username.Trim().ToLower() == dto.Username.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.UsernameAlreadyExists,
                    DbReturnValue = DbReturnValue.UsernameAlreadyExists,
                    HasSucceeded = false
                };
            }

            else if (await UserRepo.GetAnyAsync(x => x.Id != dto.Id && x.Email.Trim().ToLower() == dto.Email.Trim().ToLower() && x.IsDeleted == false))
            {
                return new DatabaseResponse
                {
                    ResponseCode = (int)DbReturnValue.EmailAlreadyExists,
                    DbReturnValue = DbReturnValue.EmailAlreadyExists,
                    HasSucceeded = false
                };
            }

            var exp = new QueryExpression<User>
            {
                Predicate = i => i.Id == dto.Id && !i.IsDeleted
            };
            var user = await UserRepo.GetFirstAsync(exp);

            if (user != null)
            {
                new UserMap().Update(user, dto);
                await UserRepo.UpdateAsync(user);
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
            var exp = new QueryExpression<User>
            {
                Predicate = i => i.Id == Id && !i.IsDeleted
            };
            var oldData = await UserRepo.GetFirstAsync(exp);
            if (oldData != null)
            {
                new MarkDelete<User>().Mark(oldData);

                await UserRepo.UpdateAsync(oldData);
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

        public async Task<DatabaseResponse> UserReport()
        {
            var exp = new QueryExpression<User>
            {
                Predicate = i => !i.IsDeleted && i.Reservations.Count() > 0,
                Includes = { x => x.Reservations },
                ThenIncludes = { "Reservations.Bike" },
                AsNoTracking = true
            };

            var data = await UserRepo.GetDataAsync(exp);

            List<UserReportDTO> returnDTO = new();

            foreach (var item in data) { returnDTO.Add(new UserMap().GetUserReportResponse(item)); }

            return new DatabaseResponse
            {
                ResponseCode = (int)DbReturnValue.RecordExists,
                DbReturnValue = DbReturnValue.RecordExists,
                HasSucceeded = true,
                Results = returnDTO
            };
        }

        #endregion
    }
}
