using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResponsibilityCentreController : ControllerBase
    {
        private readonly IRespoCentreService _centreService;

        public ResponsibilityCentreController(IRespoCentreService centreService)
        {
            _centreService = centreService;
        }

        [HttpPost ("Create")]
        public async Task<IActionResult> CreateCentre(CreateRespoCentreDto model)
        {
            var centre = await _centreService.CreateCentre(model);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre);
        }

        [HttpPatch ("Update")]
        public async Task<IActionResult> UpdateCentre(int id, CreateRespoCentreDto model)
        {
            var centre = await _centreService.UpdateCentre(id, model);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre);
        }

        [HttpDelete ("Delete")]
        public async Task<IActionResult> DeleteCentre(int id)
        {
            var centre = await _centreService.DeleteCentre(id);
            if (!centre)
            {
                return BadRequest("Request NOT successful");
            }

            return Ok("Successful");
        }

        [HttpGet ("GetCentre")]
        public async Task<IActionResult> GetCentre(int id) 
        {
            var centre = await _centreService.GetCentre(id);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre.Data);
        }
        [HttpGet ("GetCentres")]
        public async Task<IActionResult> GetCentres()
        {
            var centres = await _centreService.GetCentres();
            
            if (centres.Status == false)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
    }
}