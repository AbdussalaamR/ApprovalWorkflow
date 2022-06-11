using System.Collections.Generic;
using MEMOJET.DTOs;

namespace MEMOJET.Interfaces.Service
{
    public interface IJWTAuthenticationManager
    {
        public string GenerateToken(UserDto user, IList<RoleDto> roles);
    }
}