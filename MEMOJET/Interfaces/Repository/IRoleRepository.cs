using System.Collections.Generic;
using System.Threading.Tasks;
using MEMOJET.Identity;

namespace MEMOJET.Interfaces.Repository
{
    public interface IRoleRepository
    {
        Task<Role> AddRole(Role role);
        Task<Role> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
        Task<Role> GetRole(int id);
        Task<IList<Role>> GetRoles();
        Task<bool> RoleExist(string roleName);
        Task<Role> getRoleByName(string name);
        Task<IList<UserRole>> GetRoleByUser(string email);
        Task<IList<Role> >GetRolesByRoleIds(IList<int> roleIDs);
    }
}