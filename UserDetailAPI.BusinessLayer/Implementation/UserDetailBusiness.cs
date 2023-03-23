using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Interface;
using UserDetailAPI.DataAccessLayer.Repository.Implementation;
using AutoMapper;
using UserDetailAPI.DataAccessLayer.Entities;
using UserDetailAPI.DataAccessLayer.Repository.Interface;

namespace UserDetailAPI.BusinessLayer.Implementation
{
    public class UserDetailBusiness : IUserDetailBusiness
    {
        private readonly IUserDetailDataRepositry _userDetailDataRepositry;
        private readonly IMapper _mapper;
        public UserDetailBusiness(IUserDetailDataRepositry iuserDetailDataRepositry, IMapper mapper)
        {
            this._userDetailDataRepositry = iuserDetailDataRepositry;
            this._mapper = mapper;
        }
        public async Task<UserDetailDTO> CreateUser(UserDetailDTO userDetail)
        {
            UserDetailDTO userDetailDTO;
            try
            {
                if (userDetail is not null)
                {
                    User user = new()
                    {
                        UserId = userDetail.UserId,
                        Name = userDetail.Name,
                        Address = userDetail.Address,
                        Country = userDetail.Country,
                        ZipCode = Convert.ToInt32(userDetail.ZipCode),
                        MobileNo = Convert.ToInt64(userDetail.MobileNo),
                        CreatedDate = DateTime.Now,
                    };
                    var result = await _userDetailDataRepositry.CreateUser(user);
                    if (result != null)
                    {
                        userDetailDTO = _mapper.Map<UserDetailDTO>(result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public async Task<List<UserDetailDTO>> GetUserDetail()
        {
            List<UserDetailDTO> userDetailDTO = new();
            try
            {
                var result = await _userDetailDataRepositry.GetUserDetail();
                if (result != null)
                {
                    userDetailDTO = _mapper.Map<List<UserDetailDTO>>(result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetailDTO;
        }
        public async Task<List<UserDetailDTO>> GetUserDetailByName(string name)
        {
            List<UserDetailDTO> userDetailDTO = new();
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var result = await _userDetailDataRepositry.GetUserDetailByName(name);
                    if (result != null)
                    {
                        userDetailDTO = _mapper.Map<List<UserDetailDTO>>(result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetailDTO;
        }

        public async Task<UserDetailDTO> UpdateUserDetail(UserDetailDTO userDetail)
        {
            UserDetailDTO userDetailDTO = new UserDetailDTO();
            try
            {
                if (userDetail is not null && userDetail.UserId > 0)
                {
                    User user = new()
                    {
                        UserId = userDetail.UserId,
                        Name = userDetail.Name,
                        Address = userDetail.Address,
                        Country = userDetail.Country,
                        ZipCode = Convert.ToInt32(userDetail.ZipCode),
                        MobileNo = Convert.ToInt64(userDetail.MobileNo),
                        LastModifiedDate = DateTime.Now,
                    };
                    var result = await _userDetailDataRepositry.UpdateUserDetail(user);
                    if (result != null)
                    {
                        userDetailDTO = _mapper.Map<UserDetailDTO>(result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userDetailDTO;
        }
        public async Task<bool> DeleteUserDetail(int id)
        {
            bool isDeleted = false;
            try
            {
                if (id > 0)
                    isDeleted = await _userDetailDataRepositry.DeleteUserDetail(id);
            }
            catch (Exception)
            {
                throw;
            }
            return isDeleted;
        }

    }
}
