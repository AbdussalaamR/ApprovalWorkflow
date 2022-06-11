using System.Collections.Generic;
using MEMOJET.Entities;
using Microsoft.AspNetCore.Http;

namespace MEMOJET.DTOs
{
    public class FormDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public int RespoCentreId { get; set; }
        public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
    }

    public class CreateFormRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RespoCentreId { get; set; }
        public string Data { get; set; }

    }

    public class FillFormRequestModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int UserId { get; set; }
        public string Data { get; set; }
        
    }
    
    public class ApproveFormRequestModel
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public ApprovalAction Action { get; set; }
        public string ApprovalComment { get; set; }
    }

    public class FormResponseModel:BaseResponse
    {
        public FormDto Data { get; set; }
    }
    
    public class FormsResponseModel:BaseResponse
    {
        public IList<FormDto> Data { get; set; }
    }
}