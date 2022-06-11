using System.Security.Claims;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }
        [HttpPost ("Create")]
        public async Task<IActionResult> CreateForm(CreateFormRequestModel model)
        {
            var form = await _formService.CreateForm(model);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpPost ("FillForm")]
        public async Task<IActionResult> FillForm(FillFormRequestModel model)
        {
            model.UserId = int.Parse((User.FindFirst(ClaimTypes.NameIdentifier).ToString()));
            var form = await _formService.FillForm(model);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpPost ("ApproveForm")]
        public async Task<IActionResult> ApproveForm(ApproveFormRequestModel model)
        {
            var form = await _formService.ApproveForm(model);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpPatch ("Update")]
        public async Task<IActionResult> UpdateForm(int id, CreateFormRequestModel model)
        {
            var form = await _formService.UpdateForm(model, id);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpGet ("GetForm")]
        public async Task<IActionResult> GetForm(int id) 
        {
            var centre = await _formService.GetForm(id);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre);
        }
        
        [HttpGet ("GetForms")]
        public async Task<IActionResult> GetForms()
        {
            var centres = await _formService.GetAllForms();
            
            if (centres.Status == false)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        [HttpGet ("GetFormsByUser")]
        public async Task<IActionResult> GetFormsByUser(int id)
        {
            var centres = await _formService.GetAllFormsByUser(id);
            
            if (centres.Status == false)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        [HttpGet ("GetFormStatus")]
        public async Task<IActionResult> GetFormStatus(int id)
        {
            var centres = await _formService.GetFormStatus(id);
            
            if (centres == null)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        [HttpGet ("GetFormLocation")]
        public async Task<IActionResult> GetFormSLocation(int id)
        {
            var centres = await _formService.GetFormLocation(id);
            
            if (centres == null)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        [HttpDelete ("Delete")]
        public async Task<IActionResult> DeleteForm(int id)
        {
            var centre = await _formService.DeleteForm(id);
            if (centre == null)
            {
                return BadRequest("Request NOT successful");
            }

            return Ok("Successful");
        }
    }
}