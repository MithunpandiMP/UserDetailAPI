using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDetailAPI.BusinessLayer.DTO;

namespace UserDetailAPI.BusinessLayer.Interface
{
    public interface IUserDetailBusiness
    {
        Task<ICollection<UserDetailDTO>> GetUserDetail();
        Task<ICollection<UserDetailDTO>> GetUserDetailBySearchText(string name);
        Task<ICollection<UserDetailDTO>> GetUserDetailBySearchTexts(string name);

        Task<UserDetailDTO> GetUserDetailById(int id);
        Task<bool> CreateUser(UserDetailDTO userDetail);
        Task<bool> UpdateUserDetail(UserDetailDTO userDetail);
        Task<bool> DeleteUserDetail(int id);
    }
}
