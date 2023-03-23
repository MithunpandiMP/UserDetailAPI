using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDetailAPI.BusinessLayer.DTO
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public long ZipCode { get; set; }
        public long MobileNo { get; set; }
    }
}
