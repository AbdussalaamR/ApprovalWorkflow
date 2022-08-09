using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
        {
            private readonly IRoleService _roleService;
            private readonly IHttpContextAccessor _contextAccessor;


            public RoleController(IRoleService roleService, IHttpContextAccessor contextAccessor)
            {
                _roleService = roleService;
                _contextAccessor = contextAccessor;
            }
            //[Authorize (Roles = "Admin")]
            [HttpPost ("CreateRole")]
            public async Task<IActionResult> CreateRole(CreateRoleRequestModel model)
            {
                // var loggedInUserId=  _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                // var id = int.Parse(loggedInUserId);
                var role = await _roleService.CreateRole(model);
                return Ok(role);

            }
            
            [HttpGet ("GetById")]
            public async Task<IActionResult> GetRoleById(int id)
            {
                var role = await _roleService.GetRole(id);
                if (!role.Status)
                {
                    return BadRequest(role.Message);
                }
                else
                {
                    return Ok(role);
                }
            }
            [HttpGet ("GetByUser")]
            public async Task<IActionResult> GetRoleByUser(string name)
            {
                var role = await _roleService.GetRoleByUser(name);
                if (!role.Status)
                {
                    return BadRequest(role.Message);
                }
                else
                {
                    return Ok(role);
                }
            }
            
           
            [HttpGet ("GetAll")]
            public async Task<IActionResult> GetRoles()
            {
                var role = await _roleService.GetAllRoles();
                if (!role.Status)
                {
                    return BadRequest(role.Message);
                }
                else
                {
                    return Ok(role);
                }
            }

            [HttpPatch]
            public async Task<IActionResult> UpdateRole(CreateRoleRequestModel model, int id)
            {
                var role = await _roleService.UpdateRole(model, id);
                if (role == null)
                {
                    return BadRequest(role.Message);
                }

                return Ok(role);
            }

            [HttpDelete ("Delete")]
            public async Task<IActionResult> DeleteRole(int id)
            {
                var deleted = await _roleService.DeleteRole(id);

                return Ok(deleted);
            }
        }
}