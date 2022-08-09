using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTAuthenticationManager _authenticationManager;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IJWTAuthenticationManager authenticationManager,
            IRoleService roleService)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
            _roleService = roleService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequestModel model)
        {
            var user = await _userService.CreateUser(model);
            if (user == null)
            {
                return BadRequest(user.Message);
            }

            return Ok(user);
        }

        //[Authorize (Roles = "Admin")]
        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleRequestModel model)
        {
            var userRoles = await _userService.AssignUserRole(model);
            if (!userRoles.Status)
            {
                return BadRequest(userRoles.Message);
            }

            return Ok(userRoles);
        }

        [HttpGet("GetUsersByRoleName")]
        public async Task<IActionResult> GetUserByRoleName(string roleName)
        {
            var role = await _userService.GetUsersByRoleName(roleName);
            if (!role.Status)
            {
                return NotFound(role.Message);
            }
            else
            {
                return Ok(role.Data);
            }
        }

        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestModel model)
        {
            var updatedUser = await _userService.UpdateUser(model, id);
            if (updatedUser == null)
            {
                return BadRequest(updatedUser.Message);
            }

            return Ok(updatedUser.Message);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser([FromRoute]int id)
        {
            if (id == 0)
            {
                return BadRequest("Wrong input");
            }

            var user = await _userService.GetUserById(id);
            if (!user.Status)
            {
                return NotFound(user.Message);
            }
            else
            {
                return Ok(user.Data);
            }
        }
        
        [HttpGet ("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        
        [HttpDelete ("Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.DeleteUser(id);
            if (user == null)
            {
                return BadRequest("Request NOT successful");
            }

            return Ok("Successful");
        }
        [HttpPost ("Login")]
        public async Task<IActionResult> Login([FromBody]LogInRequestModel requestModel)
        {
            var IsValid =  await _userService.Login(requestModel.Email, requestModel.Password);
            if (IsValid.Data == null)
            {
                return BadRequest(IsValid);
            }
            var role = await _roleService.GetRoleByUser(requestModel.Email);
            var roles = role.Data;
            var token = _authenticationManager.GenerateToken(IsValid.Data, roles);
            var user = IsValid.Data;
            
            
            var log = new LoginResponse()
            {
                Token = token,
                userName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                userId = user.Id,
                Status = true,
                UserRoles = roles
            
            };
             return Ok(log);
        }
    }
}