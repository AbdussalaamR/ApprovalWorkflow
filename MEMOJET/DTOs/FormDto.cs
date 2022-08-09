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
        //public string Data { get; set; }
        public ICollection<QuestionDto> QuestionFieldForm { get; set; } = new List<QuestionDto>();
        public ICollection<UserAnswerDto> QuestionFieldUserForm { get; set; } = new List<UserAnswerDto>();
        public int RespoCentreId { get; set; }
        //public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
    }
    
    public class UserFormDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string formWith { get; set; }
        public int RespoCentreId { get; set; }
        public string RespoCentreName { get; set; }
        public string Status { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public ICollection<UserAnswerDto> QuestionFieldUserForm { get; set; } = new List<UserAnswerDto>();
        //public ICollection<UserForm> UserForms { get; set; } = new List<UserForm>();
        public ICollection<UploadedDocDto> UplodedDocs { get; set; } = new List<UploadedDocDto>();
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }

    public class CreateFormRequestModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RespoCentreId { get; set; }
        
        public ICollection<QuestionDto> QuestionFields { get; set; } = new List<QuestionDto>();

    }
    public class QuestionDto
        {
            public string FieldName { get; set; }
            public InputType InputType { get; set; }
            public string DefaultVale { get; set; }
        }
    public class UserAnswerDto
    {
        public string FieldQue { get; set; }
        public string Response { get; set; }
    }
    public class FillFormRequestModel
    {
        //public int Id { get; set; }
        public int FormId { get; set; }
        public int UserId { get; set; }
        public string FileDescription { get; set; }
        
        public ICollection<UserAnswerDto> QuestionFields { get; set; } = new List<UserAnswerDto>();
        public List<IFormFile> UplodedDocs { get; set; } = new List<IFormFile>();


    }
    
    public class ApproveFormRequestModel
    {
        //public int Id { get; set; }
        public int FormId { get; set; }
        public ApprovalAction Action { get; set; }
        public string ApprovalComment { get; set; }
        //Working on adding files by approvers
        public string ApprvFileDescription { get; set; }
        public List<IFormFile> ApprvUplodedDocs { get; set; } = new List<IFormFile>();
    }

    public class FormResponseModel:BaseResponse
    {
        public FormDto Data { get; set; }
    }
    
    public class FormsResponseModel:BaseResponse
    {
        public IList<FormDto> Data { get; set; }
    }
    
    public class UserFormResponseModel:BaseResponse
    {
        public UserFormDto Data { get; set; }
    }
    
    public class UserFormsResponseModel:BaseResponse
    {
        public IList<UserFormDto> Data { get; set; }
    }
}