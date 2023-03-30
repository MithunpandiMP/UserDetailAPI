using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDetailAPI.BusinessLayer.DTO
{
    public class UserDetailDTO
    {
        public int UserId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public long ZipCode { get; set; }
        [Required]
        public long MobileNo { get; set; }
    }
}
