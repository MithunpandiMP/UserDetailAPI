using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UserDetailAPI.BusinessLayer.DTO;
using UserDetailAPI.BusinessLayer.Implementation;
using UserDetailAPI.BusinessLayer.Interface;
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

        // GET: UserDetailController/GetAllUsers
        [HttpGet, Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            _logger.LogInformation(GetCallMessage(), "GetAllUsers");
            try
            {
               var userDetails = await _userDetail.GetUserDetail();
                if(userDetails == null)
                {
                    return NotFound();
                }
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(GetExceptionMessage(), "GetAllUsers", ex);
                return BadRequest();
            }
        }
        // GET: UserDetailController/GetUserByName/5
        [HttpPost, Route("GetUserByName")]
        public async Task<ActionResult> GetUser([FromBody] UserDetailDTO user)
        {
            _logger.LogInformation(GetCallMessage(), "GetUserByName");
            try
            {
                var userDetail = await _userDetail.GetUserDetailByName(user.Name);
                if (userDetail == null)
                {
                    return NotFound();
                }
                return Ok(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(GetExceptionMessage(), "GetUserByName", ex);
                return BadRequest();
            }
        }

        // POST: UserDetailController/CreateUser
        [HttpPost, Route("CreateUser")]
        public async Task<ActionResult> CreateUserDetail([FromBody] UserDetailDTO userDetail)
        {
            _logger.LogInformation(GetCallMessage(), "CreateUser");
            try
            {
                var user = await _userDetail.CreateUser(userDetail);
                if (user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(GetExceptionMessage(), "CreateUser", ex);
                return BadRequest();
            }
        }
        // GET: UserDetailController/UpdateUser/5
        [HttpPut, Route("UpdateUser")]
        public async Task<ActionResult> UpdateUserDetails([FromBody] UserDetailDTO userDetail)
        {
            _logger.LogInformation(GetCallMessage(), "UpdateUser");
            try
            {
                var user = await _userDetail.UpdateUserDetail(userDetail);
                if (user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(GetExceptionMessage(), "UpdateUser", ex);
                return BadRequest();
            }
        }

        private static string GetCallMessage()
        {
            return "{0} API process is started";
        }
        private static string GetExceptionMessage()
        {
            return "Exception occured while calling API method is {0}, error message is {1} ";
        }

        // GET: UserDetailController/DeleteUser/5
        [HttpDelete, Route("DeleteUser")]
        public async Task<ActionResult> DeleteUserDetails(int id)
        {
            _logger.LogInformation(GetCallMessage(), "DeleteUser");
            try
            {
                var user = await _userDetail.DeleteUserDetail(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(GetExceptionMessage(), "DeleteUser", ex);
                return BadRequest();
            }
        }
    }
}
