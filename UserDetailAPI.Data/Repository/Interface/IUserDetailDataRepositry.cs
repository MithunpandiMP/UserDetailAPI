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
        Task<ICollection<User>> GetUserDetail();
        Task<ICollection<User>> GetUserDetailBySearchText(string name);
        Task<User> GetUserDetailById(int id);
        Task<bool> CreateUser(User userDetail);
        Task<bool> UpdateUserDetail(User userDetail);
        Task<bool> DeleteUserDetail(int id);
    }
}
