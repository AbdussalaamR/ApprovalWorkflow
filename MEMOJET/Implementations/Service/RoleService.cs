using System;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Identity;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<RoleResponseModel> CreateRole(CreateRoleRequestModel model)
        {
            var nameExists =  await _roleRepository.RoleExist(model.Name);
            if (nameExists)
            {
                return new RoleResponseModel
                {
                    Message = $"{model.Name} already exists",
                    Status = false
                };
            }
            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
               // CreatedBy = id

            };
            var newRole = await _roleRepository.AddRole(role);

            if (newRole == null)
            {
                return new RoleResponseModel
                {
                    Message = $"Role NOT created",
                    Status = false,
                };
            }
            else
            {
                return new RoleResponseModel
                {
                    Data = new RoleDto
                    {
                        Description = newRole.Description,
                        id = newRole.Id,
                        Name = newRole.Name,
                    },
                    Message = $"Role created successfully",
                    Status = true
                };
            }
        }

        public async Task<RoleResponseModel> UpdateRole(CreateRoleRequestModel model, int id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = "Role not found",
                    Status = false
                };
            }
            role.Name = model.Name;
            role.Description = model.Description;
            await _roleRepository.UpdateRole(role);
            return new RoleResponseModel
            {
                Message = "Successful",
                Status = true,
                Data = new RoleDto
                {
                    id = role.Id,
                    Description = role.Description,
                    Name = role.Name
                }
            };
        }

        public async Task<RoleResponseModel> GetRole(int id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = $"Role NOT found",
                    Status = false,
                };
            }
            
            return new RoleResponseModel
            {
                Data = new RoleDto
                {
                    Description = role.Description,
                    id = role.Id,
                    Name = role.Name,
                },
                Message = $"Role retrieved successfully",
                Status = true
            };
        }
        
        public async Task<RolesResponseModel> GetRoleByUser(string email)
        {
            var userEmail = await _roleRepository.GetRoleByUser(email);
            var allRoles = userEmail.Select(g=> new RoleDto()
            {
                Name = g.Role.Name
            }).ToList();
            
                return new RolesResponseModel()
                {
                    Status = true,
                    Data = allRoles,
                    Message = "User Roles"
                };
        }

        public async Task<RoleResponseModel> GetRoleByName(string name)
        {
            var role =  await _roleRepository.getRoleByName(name);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = $"Role NOT found",
                    Status = false,
                };
            }
            
            return new RoleResponseModel
            {
                Data = new RoleDto
                {
                    Description = role.Description,
                    id = role.Id,
                    Name = role.Name,
                },
                Message = $"Role retrieved successfully",
                Status = true
            };
        }
         

        public async Task<RolesResponseModel> GetAllRoles()
        {
            var roles = await _roleRepository.GetRoles();
            var allRoles = roles.Select(x => new RoleDto
            {
                Description = x.Description,
                id = x.Id,
                Name = x.Name,
            }).ToList();
            return new RolesResponseModel
            {
                Message = "Successful",
                Status = true,
                Data = allRoles
            };
        }

        public async Task<string> DeleteRole(int id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return "Role not found" ;
            }
            role.IsDeleted = true;
           await _roleRepository.UpdateRole(role);
             return $"Role with name {role.Name} deleted successfully";
        }
    }
}