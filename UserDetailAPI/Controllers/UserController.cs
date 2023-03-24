using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Implementation;
using UserDetailAPI.BusinessLayer.Interface;
using UserDetailAPI.CustomMiddleware;
using UserDetailAPI.DataAccessLayer.Entities;

namespace UserDetailAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserDetailBusiness _userDetail;
        private readonly ILogger _logger;
        public UserController(IUserDetailBusiness userDetail, ILoggerFactory logger)
        {
            this._userDetail = userDetail;
            this._logger = logger.CreateLogger<UserController>();
        }

        [HttpGet, Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            _logger.LogInformation(GetCallMessage(), "GetAllUsers");
            return Ok(await _userDetail.GetUserDetail());
        }

        [HttpPost, Route("GetUserByName")]
        public async Task<ActionResult> GetUser([FromBody] UserDetailDTO user)
        {
            _logger.LogInformation(GetCallMessage(), "GetUserByName");
            if (string.IsNullOrEmpty(user.Name))
                return BadRequest("User Name or Address or Country required");
            var userDetail = await _userDetail.GetUserDetailByName(user.Name);
            return Ok(userDetail);
        }

        [HttpPost, Route("CreateUser")]
        public async Task<ActionResult> CreateUserDetail([FromBody] UserDetailDTO userDetail)
        {
            _logger.LogInformation(GetCallMessage(), "CreateUser");
            if (userDetail is null)
                throw new BadRequestException("User details not correct");
            return Ok(await _userDetail.CreateUser(userDetail));
        }

        [HttpPut, Route("UpdateUser")]
        public async Task<ActionResult> UpdateUserDetails([FromBody] UserDetailDTO userDetail)
        {
            _logger.LogInformation(GetCallMessage(), "UpdateUser");
            if (userDetail is null || userDetail.UserId == 0)
                throw new BadRequestException("User details not correct");
            var user = await _userDetail.UpdateUserDetail(userDetail);
            if (user is null)
                throw new NotFoundException("User not found");
            return Ok(user);
        }

        [HttpDelete, Route("DeleteUser")]
        public async Task<ActionResult> DeleteUserDetails(int id)
        {
            _logger.LogInformation(GetCallMessage(), "DeleteUser");
            if (id > 0)
            {
                var user = await _userDetail.DeleteUserDetail(id);
                if (user)
                    return Ok();
            }
            else
                throw new BadRequestException("User id not available");
            throw new NotFoundException("User not found");
        }

        [HttpGet, Route("getUserById")]
        public async Task<ActionResult> getUserById(int id)
        {
            _logger.LogInformation(GetCallMessage(), "getUserById");
            if (id > 0)
            {
                var user = await _userDetail.GetUserDetailById(id);
                if (user is not null)
                    return Ok(user);
            }
            else
                throw new BadRequestException("User id not available");
            throw new NotFoundException("User not found");
        }

        private static string GetCallMessage()
        {
            return "{0} API process is started";
        }
    }
}
