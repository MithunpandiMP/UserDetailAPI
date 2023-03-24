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
            return userDetail;
        }
        public async Task<List<UserDetailDTO>> GetUserDetail()
        {
            List<UserDetailDTO> userDetailDTO = new();
            var result = await _userDetailDataRepositry.GetUserDetail();
            if (result != null)
            {
                userDetailDTO = _mapper.Map<List<UserDetailDTO>>(result);
            }
            return userDetailDTO;
        }
        public async Task<List<UserDetailDTO>> GetUserDetailByName(string name)
        {
            List<UserDetailDTO> userDetailDTO = new();
            var result = await _userDetailDataRepositry.GetUserDetailByName(name);
            if (result != null)
            {
                userDetailDTO = _mapper.Map<List<UserDetailDTO>>(result);
            }
            return userDetailDTO;
        }

        public async Task<UserDetailDTO> GetUserDetailById(int id)
        {
            UserDetailDTO userDetailDTO = new();
            var result = await _userDetailDataRepositry.GetUserDetailById(id);
            if (result != null)
            {
                userDetailDTO = _mapper.Map<UserDetailDTO>(result);
            }
            return userDetailDTO;
        }

        public async Task<UserDetailDTO> UpdateUserDetail(UserDetailDTO userDetail)
        {
            UserDetailDTO userDetailDTO = new UserDetailDTO();
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
            return userDetailDTO;
        }
        public async Task<bool> DeleteUserDetail(int id)
        {
            return _ = await _userDetailDataRepositry.DeleteUserDetail(id);
        }

    }
}
