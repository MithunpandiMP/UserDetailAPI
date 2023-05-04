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

        public async Task<ICollection<User>> GetUserDetailBySearchTexts(string name)
        {
            var searchedUser = await _dBContext.Users
      .Where(x => EF.Functions.Like(x.Name, $"%{name}%")
                  || EF.Functions.Like(x.Country, $"%{name}%")
                  || EF.Functions.Like(x.Address, $"%{name}%"))
      .ToListAsync();
            return searchedUser;

        }

        public async Task<User> GetUserDetailById(int id)
        {
            return await _dBContext.Users.FirstAsync(x => x.UserId == id);
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
