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

        public UserController(IUserService userService, IJWTAuthenticationManager authenticationManager, IRoleService roleService)
        {
            _userService = userService;
            _authenticationManager = authenticationManager;
            _roleService = roleService;
        }
        [HttpPost ("CreateUser")]
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
        [HttpPost ("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(string email, int Ids)
        {
            var userRoles = await _userService.AssignUserRole(Ids, email);
            if (!userRoles.Status)
            {
                return BadRequest(userRoles.Message);
            }
            return Ok(userRoles);
        }
        
        [HttpGet ("GetUsersByRoleName")]
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
        [HttpPatch ("Update")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestModel model)
        {
            var updatedUser = await _userService.UpdateUser(model, id);
            if (updatedUser == null)
            {
                return BadRequest(updatedUser.Message);
            }
            return Ok(updatedUser.Message);
        }
        
        [HttpGet ("GetUser")]
        public async Task<IActionResult> GetUser(int id)
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
            return Ok(users.Data);
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
        public async Task<IActionResult> Login(string email, string password)
        {
            var IsValid =  await _userService.Login(email, password);
            if (IsValid.Data == null)
            {
                return BadRequest(IsValid.Message);
            }
            var role = await _roleService.GetRoleByUser(email);
            var roles = role.Data;
            var token = _authenticationManager.GenerateToken(IsValid.Data, roles);
            var user = IsValid.Data;
            
            
            var log = new LoginResponse()
            {
                Token = token,
            
            };
             return Ok(log);
        }
    }
}