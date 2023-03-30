using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDetailAPI.DataAccessLayer.Repository.Interface;
using UserDetailAPI.DataAccessLayer.Entities;

namespace UserDetailAPI.DataAccessLayer.Repository.Implementation
{
    public class UserDetailDataRepositry : IUserDetailDataRepositry
    {
        private readonly UserDetailDbContext _dBContext;
        public UserDetailDataRepositry(UserDetailDbContext dBContext)
        {
            this._dBContext = dBContext;
            this._dBContext.Database.EnsureCreated();
        }

        public async Task<bool> CreateUser(User userDetail)
        {
            await _dBContext.Users.AddAsync(userDetail);
            await _dBContext.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<User>> GetUserDetail()
        {
            return await _dBContext.Users.ToListAsync();
        }

        public async Task<ICollection<User>> GetUserDetailBySearchText(string name)
        {
            var allUsers = await _dBContext.Users.ToListAsync();
            var searchedUser = allUsers.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
            || x.Country.Contains(name, StringComparison.OrdinalIgnoreCase) || x.Address.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return searchedUser;
        }

        public async Task<User> GetUserDetailById(int id)
        {
            return await _dBContext.Users.FirstAsync(_ => _.UserId.Equals(id));
        }

        public async Task<bool> UpdateUserDetail(User userDetail)
        {
            _dBContext.Users.Update(userDetail);
            await _dBContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserDetail(int id)
        {
            var user = await _dBContext.Users.FindAsync(id);
            if (user is not null)
            {
                _ = _dBContext.Users.Remove(user);
                _ = await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
