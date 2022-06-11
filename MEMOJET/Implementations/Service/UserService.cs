using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Identity;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;

namespace MEMOJET.Implementations.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<UserResponseModel> CreateUser(CreateUserRequestModel model)
        {
            var userExist = await _userRepository.EmailExist(model.Email);
            if (userExist)
            {
                return new UserResponseModel
                {
                    Message = $"{model.FirstName} {model.LastName}already exists",
                    Status = false
                };
            }

            var user = new User()
            {
                CreatedOn = DateTime.UtcNow,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = model.Password,
            };
            var role = await _roleRepository.getRoleByName("Staff");
            var userRole = new UserRole
            {
                RoleId = role.Id,
                Role = role,
                User = user,
                UserId = user.Id
            };
            user.UserRoles.Add(userRole);
            var newUser =await _userRepository.AddUser(user);
           if (newUser == null)
           {
               return new UserResponseModel
               {
                   Status = false,
                   Message = "User NOT created"
               };
           }

           return new UserResponseModel
           {
               Data = new UserDto
               {
                   FirstName = newUser.FirstName,
                   LastName = newUser.LastName,
                   Id = newUser.Id,
                   Email = newUser.Email,
                   UserName = newUser.UserName
               },
               Message = $"User created successfully",
               Status = true
           };
        }
        

        public async Task<UserResponseModel> AssignUserRole(int RoleId, string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }

            var role = await _roleRepository.GetRole(RoleId);
            
            var userRoles = await _roleRepository.GetRoleByUser(user.Email);
            foreach (var rol in userRoles)
            {
                if (rol.Role == role)
                {
                    return new UserResponseModel
                    {
                        Message = "Role already exist",
                        Status = false
                    }; 
                }
            }
            var userRole = new UserRole
                {
                    RoleId = RoleId,
                    
                    UserId = user.Id,
                    User = user,
                    Role = role
                };
                user.UserRoles.Add(userRole);
                
                var userWtRole = await _userRepository.UpdateUser(user);
            if (userWtRole == null)
            {
                return new UserResponseModel
                {
                    Message = $"Unable to assign roles to {user.LastName}",
                     Status = false
                };
            }

            return new UserResponseModel
            {
                Message = $"Roles successfully assigned to {user.FirstName} {user.LastName}",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            };
        }

        public async Task<UserResponseModel> DeleteUserRole(int RoleId, string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }

            var role = await _roleRepository.GetRole(RoleId);
            
            var userRoles = await _roleRepository.GetRoleByUser(user.Email);
            foreach (var rol in userRoles)
            {
                if (rol.Role == role)
                {
                    user.UserRoles.Remove(rol);
                    await _userRepository.UpdateUser(user);
                }
            }
            return new UserResponseModel
            {
                Message = $"Roles successfully assigned to {user.FirstName} {user.LastName}",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            };
            
        }

        public async Task<UserResponseModel> UpdateUser(UpdateUserRequestModel model, int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }

            user.Password = model.Password;
            user.UserName = model.UserName;
            var updatedUser = _userRepository.UpdateUser(user);
            if (updatedUser == null)
            {
                return new UserResponseModel
                {
                    Message = "User details not updated",
                    Status = false
                };
            }
            return new UserResponseModel
            {
                Message = $"Details uploaded successfully",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            };
        }

        public async Task<UsersResponseModel> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var usersDto = users.Select(s => new UserDto
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Id = s.Id,
                Email = s.Email,
                UserName = s.UserName,
                UserRoles = s.UserRoles,
                UserForms = s.UserForms
            }).ToList();
            return new UsersResponseModel
            {
                Data = usersDto,
                Message = "Successful",
                Status = true
            };
        }

        public async Task<UsersResponseModel> GetUsersByRoleName(string roleName)
        {
            var users = await _userRepository.GetUserByRole(roleName);
            var usersDto = users.Select(s => new UserDto
            {
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Id = s.User.Id,
                Email = s.User.Email,
                UserName = s.User.UserName,
                
            }).ToList();
            return new UsersResponseModel
            {
                Data = usersDto,
                Message = "Successful",
                Status = true
            };
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }
            return new UserResponseModel
            {
                Message = $"Successful",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            };
        }

        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }
            return new UserResponseModel
            {
                Message = $"Successful",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            };
        }

        public async Task<string> DeleteUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return "User not found";
            }

            user.IsDeleted = true;
           await _userRepository.UpdateUser(user);
            return "User deleted successfully";
        }

        public async Task<UserResponseModel> Login(string email, string password)
        {
            var user = await _userRepository.GetLoogedInUser(email, password);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }

            return new UserResponseModel
            {
                Message = $"Successful",
                Status = true,
                Data = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    UserRoles = user.UserRoles
                }
            };
        }
    }
}