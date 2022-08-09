using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IUserService
    {
        public Task<UserResponseModel> CreateUser(CreateUserRequestModel model);
        public Task<UserResponseModel> AssignUserRole(AssignRoleRequestModel model);
        public Task<UserResponseModel> DeleteUserRole(int RoleId, string email);
        public Task<UserResponseModel> UpdateUser(UpdateUserRequestModel model, int id);
        public Task<UsersResponseModel> GetUsers();
        public Task<UsersResponseModel> GetUsersByRoleName(string roleName);
        public Task<UserResponseModel> GetUserById(int Id);
        public Task<UserResponseModel> GetUserByEmail(string email);
        public Task<string> DeleteUser(int id);
        public Task<UserResponseModel> Login(string email, string password);

    }
}