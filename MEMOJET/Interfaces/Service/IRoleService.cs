using System.Threading.Tasks;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IRoleService
    {
        public Task<RoleResponseModel> CreateRole(CreateRoleRequestModel model);
        public Task<RoleResponseModel> UpdateRole(CreateRoleRequestModel model, int id);
        public Task<RoleResponseModel> GetRole(int id);
        public Task<RolesResponseModel> GetAllRoles();
        public Task<string> DeleteRole(int id);
        public Task<RolesResponseModel> GetRoleByUser(string email);
        public Task<RoleResponseModel> GetRoleByName(string name);
    }
}