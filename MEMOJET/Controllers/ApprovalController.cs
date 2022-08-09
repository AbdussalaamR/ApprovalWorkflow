using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApprovalController : ControllerBase
    {
        private readonly IApprovalService _approvalService;

        public ApprovalController(IApprovalService approvalService)
        {
            _approvalService = approvalService;
        }
        [HttpPost ("Create")]
        public async Task<IActionResult> CreateApproval(CreateApprovalRequestModel model)
        {
            var approval = await _approvalService.CreateApproval(model);
            if (approval == null)
            {
                return BadRequest(approval.Message);
            }

            return Ok(approval);
        }
        
        [HttpPatch ("Update")]
        public async Task<IActionResult> UpdateApproval(int id, CreateApprovalRequestModel model)
        {
            var approval = await _approvalService.UpdateApproval(model, id);
            if (approval == null)
            {
                return BadRequest(approval.Message);
            }

            return Ok(approval);
        }
        
        [HttpGet ("GetApproval")]
        public async Task<IActionResult> GetApproval(int id) 
        {
            var approval = await _approvalService.GetApproval(id);
            if (approval == null)
            {
                return BadRequest(approval.Message);
            }

            return Ok(approval);
        }
        
        [HttpGet ("GetApprovals")]
        public async Task<IActionResult> GetApprovals()
        {
            var approvals = await _approvalService.GetAllApprovals();
            
            if (approvals.Status == false)
            {
                return BadRequest("No data");
            }

            return Ok(approvals);
        }
        
        [HttpGet ("GetApprovalsInCentre/{centreId}")]
        public async Task<IActionResult> GetApprovalsInCentre([FromRoute] int centreId)
        {
            var approvals = await _approvalService.GetApprovalsInRespoCentre(centreId);
            
            if (approvals == null)
            {
                return BadRequest("No data found");
            }

            return Ok(approvals);
        }
        
        [HttpDelete ("Delete")]
        public async Task<IActionResult> DeleteApproval(int id)
        {
            var approval = await _approvalService.DeleteApproval(id);
            if (approval == null)
            {
                return BadRequest("Request NOT successful");
            }

            return Ok(approval);
        } 
    }
}