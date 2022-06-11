using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Identity;
using MEMOJET.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class RoleRepository:IRoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Role> AddRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRole(Role role)
        { 
            _context.Roles.Remove(role);
            await  _context.SaveChangesAsync();
            return true;
        }

        public async Task<Role> GetRole(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);
            return role;
        }

        public async Task<IList<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // public IList<Role> GetSelectedUsersByRoles(IList<int> userIds)
        // {
        //     var roles = _context.Roles.Include(x => x.UserRoles.
        //         Where(x => userIds.Contains(x.UserId))).ToList();
        //     return roles;
        // }
        public async Task<IList<Role>> GetRolesByRoleIds(IList<int> roleIDs)
        {
            var roles =  await _context.Roles.Where(x => roleIDs.Contains(x.Id)).ToListAsync();
            return roles;
        }
        
        public async Task<Role> getRoleByName(string name)
        {
            var role = await _context.Roles.Where(x => x.Name == name).FirstOrDefaultAsync();
            return role;
        }

        public async Task<IList<UserRole>> GetRoleByUser(string email)
        {
            // var role = _context.UserRoles.Include(x => x.User).
            //     Include(x => x.Role).Where(x => x.User.Email == email).ToList()
            var role = await _context.UserRoles.Include(x => x.Role)
                .Where(x => x.User.Email == email).ToListAsync();
            return role;
        }
        public async Task<bool> RoleExist(string roleName)
        {
            return await _context.Roles.AnyAsync(x => x.Name == roleName);
        }
    }
}