using UserDetailAPI.DataAccessLayer.Entities;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Implementation;

namespace UserDetailAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, UserDetailDTO>();
            CreateMap<User, UserDetailDTO>().ReverseMap();
        }
    }
}
