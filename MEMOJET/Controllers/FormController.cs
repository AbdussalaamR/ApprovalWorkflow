using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace MEMOJET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IuploadedDocService _docService;
        private readonly IApprovalService _approvalService;

        public FormController(IFormService formService, IuploadedDocService docService, IApprovalService approvalService)
        {
            _formService = formService;
            _docService = docService;
            _approvalService = approvalService;
        }
        [HttpPost ("Create")]
        public async Task<IActionResult> CreateForm([FromForm]CreateFormRequestModel model)
        {
            int count = 0;
            List<string> formLabel = new List<string>();
            List<string> fieldType = new List<string>();
            List<string> defaultLabel = new List<string>();
            var formData = Request.Form.Keys;
            foreach (var item in formData)
            {
                count++;
                var x = Request.Form[item];
                if (count == 4)
                {
                    var arr = x.ToString().Split(',');
                    foreach (var st in arr)
                    {
                        formLabel.Add(st);
                    }
                }
                if (count == 5 )
                {
                    var arr = x.ToString().Split(',');
                    foreach (var st in arr)
                    {
                        fieldType.Add(st);
                    }
                }
                if (count == 6)
                {
                    var arr = x.ToString().Split(',');
                    foreach (var st in arr)
                    {
                        defaultLabel.Add(st);
                    }
                }
            }

            for (int i = 0; i < fieldType.Count; i++)
            {
                var mod = new QuestionDto
                {
                    FieldName =  formLabel[i],
                    InputType = (InputType)int.Parse(fieldType[i]),
                    DefaultVale =  defaultLabel[i]
                    
                };
                model.QuestionFields.Add(mod);
            }
            var form = await _formService.CreateForm(model);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        
        
        [HttpPost ("FillForm")]
        public async Task<IActionResult> FillForm([FromForm] FillFormRequestModel model)
        {
            var inputs = Request.Form.Keys;
            var count = 0;
            var fieldQuest = new List<string>();
            var fieldResponse = new List<string>();
            
            foreach (var y in inputs)
            {
                var x = Request.Form[y];
                if (count == 2)
                {
                    var arr = x.ToString().Split(',');
                    foreach (var st in arr)
                    {
                        fieldQuest.Add(st);
                    }
                }

                if (count == 3)
                {
                    var arr = x.ToString().Split(',');
                    foreach (var st in arr)
                    {
                        fieldResponse.Add(st);
                    }   
                }

                count++;
                 
            }

            for (int j = 0; j < fieldQuest.Count; j++)
            {
                var userAnswe = new UserAnswerDto
                {
                    FieldQue = fieldQuest[j],
                    Response = fieldResponse[j]
                };
                model.QuestionFields.Add((userAnswe));
            }
            var form = await _formService.FillForm(model);
            
            if (form == null)
                {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpPost ("ApproveForm")]
        public async Task<IActionResult> ApproveForm([FromForm] ApproveFormRequestModel model)
        {
            var form = await _formService.ApproveForm(model);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpPut ("Update")]
        public async Task<IActionResult> UpdateForm(int id, CreateFormRequestModel model)
        {
            var form = await _formService.UpdateForm(model, id);
            if (form == null)
            {
                return BadRequest(form.Message);
            }

            return Ok(form);
        }
        
        [HttpGet ("GetForm/{id}")]
        public async Task<IActionResult> GetForm([FromRoute] int id) 
        {
            var centre = await _formService.GetForm(id);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre);
        }
        
        [HttpGet ("GetUserForm/{id}")]
        public async Task<IActionResult> GetUserForm([FromRoute] int id) 
        {
            var centre = await _formService.GetUserForm(id);
            if (centre == null)
            {
                return BadRequest(centre.Message);
            }

            return Ok(centre);
        }
         
        [HttpGet ("DownloadFileFromDatabase/{id}")]
        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {
            var file = await _docService.GetDoc(id);
            
            if (file == null) 
            {
                return BadRequest("No file found");
            }
            return File(file.Data.Data, "application/octet-stream", "document"+file.Data.Extension);
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
        
        [HttpGet ("GetFormsByUser/{id}")]
        public async Task<IActionResult> GetFormsByUser([FromRoute] int id)
        {
            var centres = await _formService.GetAllFormsByUser(id);
            
            if (centres.Status == false)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        
        [HttpGet ("GetFormsByApproval/{id}")]
        public async Task<IActionResult> GetFormsByApproval([FromRoute] int id)
        {
            var userApprovalIds = await _approvalService.GetApprovalIdsOfUser(id);
            var centres = await _formService.GetAllFormsByApproval(id, userApprovalIds);
            
            if (centres.Status == false)
            {
                return BadRequest("No data found");
            }

            return Ok(centres);
        }
        
        // [HttpGet ("GetAllFormsByApproval/{id}")]
        // public async Task<IActionResult> GetAllFormsByApproval([FromRoute] int id)
        // {
        //     var centres = await _formService.GetTotalFormsByApproval(id);
        //     
        //     if (centres.Status == false)
        //     {
        //         return BadRequest("No data found");
        //     }
        //
        //     return Ok(centres);
        // }
        
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