using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Interface;
using AutoMapper;
using UserDetailAPI.DataAccessLayer.Entities;
using UserDetailAPI.DataAccessLayer.Repository.Interface;
using System.Collections.Generic;

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

        public async Task<bool> CreateUser(UserDetailDTO userDetail)
        {
            User user = _mapper.Map<User>(userDetail);
            user.CreatedDate = DateTime.Now;
             return await _userDetailDataRepositry.CreateUser(user);
        }

        public async Task<ICollection<UserDetailDTO>> GetUserDetail()
        {
            var result = await _userDetailDataRepositry.GetUserDetail();
            if (result.Count > 0)
                return _mapper.Map<ICollection<UserDetailDTO>>(result);
            else
                return new List<UserDetailDTO>();
        }

        public async Task<ICollection<UserDetailDTO>> GetUserDetailBySearchText(string name)
        {
            List<UserDetailDTO> userDetailDTO = new();
            var result = await _userDetailDataRepositry.GetUserDetailBySearchText(name);
            if (result.Count > 0)
                return _mapper.Map<ICollection<UserDetailDTO>>(result);
            else
                return new List<UserDetailDTO>();
        }

        public async Task<UserDetailDTO> GetUserDetailById(int id)
        {
            UserDetailDTO userDetailDTO = new();
            var result = await _userDetailDataRepositry.GetUserDetailById(id);
            if (result != null)
                userDetailDTO = _mapper.Map<UserDetailDTO>(result);
            return userDetailDTO;
        }

        public async Task<bool> UpdateUserDetail(UserDetailDTO userDetail)
        {
            User user = _mapper.Map<User>(userDetail);
            user.LastModifiedDate = DateTime.Now;
            return await _userDetailDataRepositry.UpdateUserDetail(user);
        }

        public async Task<bool> DeleteUserDetail(int id)
        {
            return _ = await _userDetailDataRepositry.DeleteUserDetail(id);
        }

    }
}
