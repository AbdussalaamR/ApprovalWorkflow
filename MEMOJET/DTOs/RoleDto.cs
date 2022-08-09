using System.Collections.Generic;
using MEMOJET.Identity;

namespace MEMOJET.DTOs
{
    public class RoleDto
    {
        public int id { get; set; }
        public string Name{ get; set; }
        public  string Description{ get; set; }
        
    }

    public class CreateRoleRequestModel
    {
        
        public string Name{ get; set; }
        public  string Description{ get; set; }
    }

    public class RoleResponseModel:BaseResponse
    {
        public RoleDto Data { get; set; }
    }

    public class RolesResponseModel : BaseResponse
    {
        public IList<RoleDto> Data { get; set; }
    }
}