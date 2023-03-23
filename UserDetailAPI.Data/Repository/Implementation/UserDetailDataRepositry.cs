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
            try
            {
                await _dBContext.Users.AddAsync(userDetail);
                await _dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return userDetail;
        }
        public async Task<List<User>> GetUserDetail()
        {
            try
            {
                return await _dBContext.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<User>> GetUserDetailByName(string name)
        {
            try
            {
                var allUsers = await _dBContext.Users.ToListAsync();
                var searchedUser = allUsers.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                || x.Country.Contains(name, StringComparison.OrdinalIgnoreCase) || x.Address.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                if (searchedUser.Count == 0)
                    return allUsers;
                else
                    return searchedUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> UpdateUserDetail(User userDetail)
        {
            try
            {
                    var Obj = _dBContext.Users.Update(userDetail);
                    var result = await _dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
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
