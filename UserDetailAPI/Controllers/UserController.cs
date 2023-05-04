using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Interface;
using UserDetailAPI.CustomMiddleware;

namespace UserDetailAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserDetailBusiness _userDetail;
        public UserController(IUserDetailBusiness userDetail)
        {
            this._userDetail = userDetail;
        }
        [Benchmark]
        [HttpGet, Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return Ok(await _userDetail.GetUserDetail());
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            }
        }

        [HttpPost, Route("SearchUser")]
        public async Task<IActionResult> SearchUser([FromBody] UserSearchDTO userSearchDTO)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                if (string.IsNullOrEmpty(userSearchDTO.SearchText))
                    return BadRequest("SearchText required");
                var userDetail = await _userDetail.GetUserDetailBySearchText(userSearchDTO.SearchText);
                return Ok(userDetail);
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            }

        }
        [HttpPost, Route("SearchUsers")]
        public async Task<IActionResult> SearchUsers([FromBody] UserSearchDTO userSearchDTO)
        {

            var sw = Stopwatch.StartNew();
            try
            {
                if (string.IsNullOrEmpty(userSearchDTO.SearchText))
                    return BadRequest("SearchText required");
                var userDetail = await _userDetail.GetUserDetailBySearchTexts(userSearchDTO.SearchText);
                return Ok(userDetail);
            }
            finally
            {
                sw.Stop();
                Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            }
        }

        [HttpPost, Route("CreateUser")]
        public async Task<IActionResult> CreateUserDetail([FromBody] UserDetailDTO userDetail)
        {
            if (ModelState.IsValid && userDetail.UserId == 0)
                return Ok(await _userDetail.CreateUser(userDetail));
            else
                return BadRequest("Invalid user details");
        }

        [HttpPut, Route("UpdateUser")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserDetailDTO userDetail)
        {
            if (ModelState.IsValid && userDetail.UserId > 0)
            {
                var user = await _userDetail.UpdateUserDetail(userDetail);
                return Ok(user);
            }
            else
                return BadRequest("User details not correct");
        }

        [HttpDelete, Route("DeleteUser")]
        public async Task<IActionResult> DeleteUserDetails(int id)
        {
            if (id > 0)
            {
                bool deleted = await _userDetail.DeleteUserDetail(id);
                if (deleted)
                    return Ok();
                else
                    return NotFound("User not found");
            }
            else
                return BadRequest("User id not available in the request");
        }

        [HttpGet, Route("GetUserById")]
        public async Task<IActionResult> getUserById(int id)
        {
            if (id > 0)
            {
                var user = await _userDetail.GetUserDetailById(id);
                if (user is not null)
                    return Ok(user);
                else
                    return NotFound("User not found");
            }
            else
                return BadRequest("User id not available");
        }
    }
}
