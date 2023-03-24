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

        public async Task<User> CreateUser(User userDetail)
        {
            await _dBContext.Users.AddAsync(userDetail);
            await _dBContext.SaveChangesAsync();
            return userDetail;
        }

        public async Task<List<User>> GetUserDetail()
        {
            return await _dBContext.Users.ToListAsync();
        }

        public async Task<List<User>> GetUserDetailByName(string name)
        {
            var allUsers = await _dBContext.Users.ToListAsync();
            var searchedUser = allUsers.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
            || x.Country.Contains(name, StringComparison.OrdinalIgnoreCase) || x.Address.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return searchedUser;
        }
        public async Task<User> GetUserDetailById(int id)
        {
            return await _dBContext.Users.FindAsync(id);
        }
        public async Task<User> UpdateUserDetail(User userDetail)
        {
            var Obj = _dBContext.Users.Update(userDetail);
            var result = await _dBContext.SaveChangesAsync();
            return userDetail;
        }

        public async Task<bool> DeleteUserDetail(int id)
        {
            var user = await _dBContext.Users.FindAsync(id);
            if (user is not null)
            {
                var Obj = _dBContext.Users.Remove(user);
                var result = await _dBContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
