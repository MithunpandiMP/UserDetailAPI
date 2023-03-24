using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDetailAPI.DataAccessLayer.Entities;

namespace UserDetailAPI.DataAccessLayer.Repository.Interface
{
    public interface IUserDetailDataRepositry
    {
        Task<List<User>> GetUserDetail();
        Task<List<User>> GetUserDetailByName(string name);
        Task<User> GetUserDetailById(int id);
        Task<User> CreateUser(User userDetail);
        Task<User> UpdateUserDetail(User userDetail);
        Task<bool> DeleteUserDetail(int id);
    }
}
