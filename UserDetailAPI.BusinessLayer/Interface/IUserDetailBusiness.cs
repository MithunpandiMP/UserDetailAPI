﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDetailAPI.BusinessLayer.DTO;

namespace UserDetailAPI.BusinessLayer.Interface
{
    public interface IUserDetailBusiness
    {
        Task<List<UserDetailDTO>> GetUserDetail();
        Task<UserDetailDTO> GetUserDetailById(int id);
        Task<UserDetailDTO> CreateUser(UserDetailDTO userDetail);
        Task<UserDetailDTO> UpdateUserDetail(UserDetailDTO userDetail);
        Task<bool> DeleteUserDetail(int id);
    }
}