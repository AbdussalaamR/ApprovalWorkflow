using System.Collections;
using System.Collections.Generic;
using MEMOJET.Contract;
using MEMOJET.DTOs;

namespace MEMOJET.Entities
{
    public class ResponsibilityCentre:AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ApprovalRespoCentre> ApprovalResponsibilityCentres { get; set; } =
             new List<ApprovalRespoCentre>();
    }
    
}