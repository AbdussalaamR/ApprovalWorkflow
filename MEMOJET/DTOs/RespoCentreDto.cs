using System.Collections.Generic;
using MEMOJET.Entities;

namespace MEMOJET.DTOs
{
    public class RespoCentreDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ApprovalRespoCentre> ApprovalResponsibilityCentres { get; set; } =
            new List<ApprovalRespoCentre>();
    }
    
    public class CreateRespoCentreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
    
    public class RespoCentreResponse : BaseResponse
    {
        public RespoCentreDto Data { get; set; }
    }
    
    public class RespoCentresResponse : BaseResponse
    {
        public IList<RespoCentreDto> Data { get; set; }
    }
}