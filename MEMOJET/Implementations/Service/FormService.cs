using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MEMOJET.DTOs;
using MEMOJET.Entities;
using MEMOJET.Implementations.Service.EMailService;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Hosting;

namespace MEMOJET.Implementations.Service
{
    public class FormService:IFormService
    {
        private readonly IFormRepo _formRepo;
        private readonly IUserRepository _userRepo;
        private readonly IApprovalRepo _approvalRepo;
        private readonly ICommentRepo _commentRepo;
        private readonly IUserformRepo _userformRepo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IResponsibilityCentreRepo _centreRepo;
        private readonly IUploadedDocRepo _docRepo;
        private readonly IMailService _mailService;

        public FormService(IWebHostEnvironment hostEnvironment, IResponsibilityCentreRepo centreRepo, IFormRepo formRepo,
            IUserRepository userRepo, IApprovalRepo approvalRepo, ICommentRepo commentRepo, IUserformRepo userformRepo,
            IUploadedDocRepo docRepo, IMailService mailService)
        {
            _docRepo = docRepo;
            _formRepo = formRepo;
            _userRepo = userRepo;
            _approvalRepo = approvalRepo;
            _commentRepo = commentRepo;
            _userformRepo = userformRepo;
            _hostEnvironment = hostEnvironment;
            _centreRepo = centreRepo;
            _mailService = mailService;
        }

        public async Task<FormResponseModel> CreateForm(CreateFormRequestModel model)
        {
            var formExists = await _formRepo.FormExists(model.Name);
            if (formExists)
            {
                return new FormResponseModel
                {
                    Message = $"Form with title {model.Name} already exists",
                };
            }

            var form = new Form
            {
                Description = model.Description,
                Title = model.Name,
                RespoCentreId = model.RespoCentreId,
                Data = JsonSerializer.Serialize(model.QuestionFields)

            };
            var newForm = await _formRepo.CreateForm(form);
            if (newForm == null)
            {
                return new FormResponseModel
                {
                    Message = $"Unable to create form",
                };
            }
            return new FormResponseModel
            {
                Message = $"{model.Name.ToUpper()} created successfully",
                Status = true,
                Data = new FormDto
                {
                    Id = newForm.Id,
                    Description = newForm.Description,
                    QuestionFieldForm = JsonSerializer.Deserialize<List<QuestionDto>>(newForm.Data),
                    Name = newForm.Title,
                    RespoCentreId = newForm.RespoCentreId
                }
            };
        }
 
        public async Task<FormResponseModel> FillForm(FillFormRequestModel model)
        {//Creates userform with users inputs in form filled
            var form = await _formRepo.Getform(model.FormId);
            var user = await _userRepo.GetUser(model.UserId);
            var centre = await _centreRepo.GetCentre(form.RespoCentreId);
            var userForm = new UserForm
            {
                Form = form,
                UserId = model.UserId,
                User = user,
                CreatedBy = user.Id,
                FormId = form.Id,
                Data = JsonSerializer.Serialize(model.QuestionFields),
                FormType = form.Title,
                ResponsibilityCentreId = form.RespoCentreId,
                ApprovalStatus = ApprovalStatus.InProgress,
                
            };
            var formApprovals = await _approvalRepo.GetApprovalsInCentre(form.RespoCentreId);
            var orderedApprovals = formApprovals.OrderBy(x => x.Sequence).ToList();
            
            userForm.ApprovalId = orderedApprovals[0].Id;
            //userForm.UserId = orderedApprovals[0].UserId;
            userForm.ArrivedApproval = DateTime.UtcNow;
            
              user.UserForms.Add(userForm);
             await _userRepo.UpdateUser(user);
            
            List<UploadedDoc> myDocs = new List<UploadedDoc>();
            Dictionary<string, byte[]> attachedDocs = new Dictionary<string, byte[]>();
            foreach (var file in  model.UplodedDocs)
            {
                var myDocName = "";
                if (file != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var path = _hostEnvironment.WebRootPath;
                    var docPath = Path.Combine(path, "myDocs");
                    Directory.CreateDirectory(docPath);
                    var docType = file.ContentType;
                    var extension = Path.GetExtension(file.FileName);
                    myDocName = $"{Guid.NewGuid()}.{docType}";
                    var fullPath = Path.Combine(docPath, myDocName);

                    var doc = new UploadedDoc
                    {
                        Name = myDocName,
                        Extension = extension,
                        Description = model.FileDescription,
                        FileType = docType,
                        UploadedBy = user.Id,
                        UserFormId = userForm.Id,
                        UserForm = userForm

                    };
                     
                    using(var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        doc.Data = dataStream.ToArray();
                    }
                    var myDoc = await _docRepo.CreateDoc(doc);
                     myDocs.Add(myDoc);
                     attachedDocs.Add(doc.Name, doc.Data);
                }
                
            }
            userForm.UplodedDocs = myDocs;
            user.UserForms.Add(userForm);
            await _userRepo.UpdateUser(user);
            var mailRecipient = await _userRepo.GetUser(orderedApprovals[0].UserId);
            var approverMailRequest = new MailRequest
            {
                Subject = "Notification of pending approval request",
                ToName = $"{mailRecipient.FirstName} {mailRecipient.LastName}",
                AttachedDocs = attachedDocs,
                ToEmail = $"{mailRecipient.Email}",
                HtmlContent = "<html><body><h1>You have a new approval request {{params.parameter}}</h1></body></html>",
            }; 
            
            var senderMailRequest = new MailRequest
            {
                Subject = "Request Initiation",
                ToName = $"{user.FirstName} {user.LastName}",
                AttachedDocs = attachedDocs,
                ToEmail = $"{user.Email}",
                HtmlContent = "<html><body><h1>Your request has been successfully initiated and is being processed. Your request is presently with {mailRecipient.FirstName} {mailRecipient.LastName} {{params.parameter}}</h1></body></html>",
            }; 
            _mailService.SendEMailAsync(approverMailRequest);
            _mailService.SendEMailAsync(senderMailRequest);
            return new FormResponseModel
            {
                Message = "Successfully submitted",
                Status = true,
                Data = new FormDto
                { 
                    Description = form.Description,
                    Id = userForm.Id,
                    Name = userForm.FormType,
                    QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(userForm.Data),
                    RespoCentreId = userForm.ResponsibilityCentreId,
                }
            };
        }

        public async Task<FormResponseModel> ApproveForm(ApproveFormRequestModel model)
        {
            var userform = await _formRepo.GetUserform(model.FormId);
            userform.ApprovalAction = model.Action;
            var comment = new Comment
            {
                ApprovalComment = model.ApprovalComment,
                UserFormId = userform.Id,
                ApprovalId = userform.ApprovalId,
                UserForm = userform
            };
            
            var formApprovals = await _approvalRepo
                .GetApprovalsInCentre(userform.ResponsibilityCentreId);
            var orderedApprovals = formApprovals.OrderBy(x => x.Sequence).ToList();
            var lenght = orderedApprovals.Count;
            
            var currentApproval = orderedApprovals.Where(x => x.Id == userform.ApprovalId).FirstOrDefault();
            var index = orderedApprovals.IndexOf(currentApproval);
            if (orderedApprovals[lenght-1] == currentApproval)
            {
                if (userform.ApprovalAction == ApprovalAction.Approved)
                {
                    userform.ApprovalStatus = ApprovalStatus.Approved;
                }
                else if (userform.ApprovalAction == ApprovalAction.Rejected)
                {
                    userform.ApprovalStatus = ApprovalStatus.Rejected;
                }
                
                else if (userform.ApprovalAction == ApprovalAction.Revise)
                {
                    userform.ApprovalId = orderedApprovals[index - 1].Id;
                    userform.ArrivedApproval = DateTime.UtcNow;
                    userform.ApprovalStatus = ApprovalStatus.Revise;
                }
            }
            
            else if (orderedApprovals[0] == currentApproval && userform.ApprovalAction == ApprovalAction.Revise)
            {
                userform.ApprovalStatus = ApprovalStatus.Revise;
                    
            }
            
            else if (userform.ApprovalAction == ApprovalAction.Approved)
            {
                userform.ApprovalId = orderedApprovals[index + 1].Id;
                userform.ArrivedApproval = DateTime.UtcNow;
            } 
            else if (userform.ApprovalAction == ApprovalAction.Rejected)
            {
                userform.ApprovalStatus = ApprovalStatus.Rejected;
            }
            else if (userform.ApprovalAction == ApprovalAction.Revise)
            {
                userform.ApprovalId = orderedApprovals[index - 1].Id;
                userform.ApprovalStatus = ApprovalStatus.Revise;
                userform.ArrivedApproval = DateTime.UtcNow;
            }

            await _userformRepo.UpdateUserForm(userform);
            var newComment = await _commentRepo.CreateComment(comment);

            List<UploadedDoc> myDocs = new List<UploadedDoc>();
            foreach (var file in  model.ApprvUplodedDocs)
            {
                var myDocName = "";
                if (file != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var path = _hostEnvironment.WebRootPath;
                    var docPath = Path.Combine(path, "myDocs");
                    Directory.CreateDirectory(docPath);
                    var docType = file.ContentType;
                    var extension = Path.GetExtension(file.FileName);
                    myDocName = $"{Guid.NewGuid()}.{docType}";
                    var fullPath = Path.Combine(docPath, myDocName);

                    var doc = new UploadedDoc
                    {
                        Name = myDocName,
                        Extension = extension,
                        Description = model.ApprvFileDescription,
                        FileType = docType,
                        UploadedBy = currentApproval.Id,
                        UserFormId = userform.Id,
                        UserForm = userform,
                        Comment = newComment,
                        CommentId = newComment.Id

                    };
                     
                    using(var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        doc.Data = dataStream.ToArray();
                    }
                    var myDoc = await _docRepo.CreateDoc(doc);
                    myDocs.Add(myDoc);
                }
                
            }
            newComment.UplodedDocs = myDocs;
            await _commentRepo.UpdateComment(newComment);
            
            
            
            
            
            
            
            var approver = await _approvalRepo.GetApproval(userform.ApprovalId);
            var mailRecipient = await _userRepo.GetUser(approver.UserId);
            var user = await _userRepo.GetUser(userform.CreatedBy?? userform.UserId);
            Dictionary<string, byte[]> attachedDocs = new Dictionary<string, byte[]>();
            foreach (var file in userform.UplodedDocs)
            {
                if (file != null)
                {
                    attachedDocs.Add(file.Name, file.Data); 
                }
            }
            var approverMailRequest = new MailRequest
            {
                Subject = "Notification of pending approval request",
                ToName = $"{mailRecipient.FirstName} {mailRecipient.LastName}",
                AttachedDocs = attachedDocs,
                ToEmail = $"{mailRecipient.Email}",
                HtmlContent = "<html><body><h1>You have a new approval request {{params.parameter}}</h1></body></html>",
            }; 
            
            var senderMailRequest = new MailRequest
            {
                Subject = "Request Initiation",
                ToName = $"{user.FirstName} {user.LastName}",
                ToEmail = $"{user.Email}",
                HtmlContent = "<html><body><h1>Your initiated request is presently with {mailRecipient.FirstName} {mailRecipient.LastName} {{params.parameter}}</h1></body></html>",
            }; 
            _mailService.SendEMailAsync(approverMailRequest);
            _mailService.SendEMailAsync(senderMailRequest);
            return new FormResponseModel
            {
                Message = "Success",
                Status = true,
                Data = new FormDto
                {
                    Id = userform.Id,
                    Name = userform.FormType,
                    QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(userform.Data),
                    RespoCentreId = userform.ResponsibilityCentreId
                }
            };
        }

        public async Task<FormResponseModel> UpdateForm(CreateFormRequestModel model, int id)
        {
            var form = await _formRepo.Getform(id);
            if (form == null)
            {
                return new FormResponseModel
                {
                    Message = "Form not found"
                };
            }

            form.Title = model.Name;
            form.Description = model.Description;
            form.Data = JsonSerializer.Serialize(model.QuestionFields);
            form.RespoCentreId = model.RespoCentreId;
            var updatedForm = await _formRepo.UpdateForm(form);
            if (updatedForm == null)
            {
                return new FormResponseModel
                {
                    Message = "Unable to update form"
                };
            }

            return new FormResponseModel
            {
                Message = "Successfully updated",
                Status = true,
                Data = new FormDto
                {
                    Id = updatedForm.Id,
                    Description = updatedForm.Description,
                    QuestionFieldForm = JsonSerializer.Deserialize<List<QuestionDto>>(updatedForm.Data),
                    Name = updatedForm.Title,
                    RespoCentreId = updatedForm.RespoCentreId
                }
            };
        }

        public async Task<FormResponseModel> GetForm(int id)
        {
            var form = await _formRepo.Getform(id);
            if (form == null)
            {
                return new FormResponseModel
                {
                    Message = "Form not found",
                    Status = true
                };
            }

            return new FormResponseModel
            {
                Data = new FormDto
                {
                    Id = form.Id,
                    Name = form.Title,
                    Description = form.Description,
                    QuestionFieldForm = JsonSerializer.Deserialize<List<QuestionDto>>(form.Data),
                    RespoCentreId = form.RespoCentreId,
                    //UserForms = form.UserForms,
                },
                Message = "Retrieved successfully",
                Status = true
            };
        }

        public async Task<FormsResponseModel> GetAllForms()
        {
            var forms = await _formRepo.GetForms();
            var formsDto = forms.Select(g => new FormDto
            {
                Name = g.Title,
                Description = g.Description,
                QuestionFieldForm = JsonSerializer.Deserialize<List<QuestionDto>>(g.Data),
                Id = g.Id ,
                RespoCentreId = g.RespoCentreId,
                //UserForms = g.UserForms
            }).ToList();
            return new FormsResponseModel
            {
                Data = formsDto,
                Message = "Retrieved",
                Status = true,
            };
        }

        public async Task<UserFormsResponseModel> GetAllFormsByUser(int userId)
        { 
            var forms = await _formRepo.GetFormsByUser(userId);
            var user = await _userRepo.GetUser(userId);
            var formsDto = new List<UserFormDto>();
            foreach (var g in forms)
            {
                var approval = await _approvalRepo.GetApproval(g.ApprovalId);
                var approvalwithName = await _userRepo.GetUser(approval.UserId);
                var formsDTo = new UserFormDto
                {
                    Name = g.FormType,
                    QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(g.Data),
                    RespoCentreId = g.ResponsibilityCentreId,
                    Id = g.Id,
                    ApprovalStatus = g.ApprovalStatus,
                    Data = g.Data,
                    UplodedDocs = g.UplodedDocs.Select(g => new UploadedDocDto
                    {
                        Name = g.Name,
                        Id = g.Id,
                        Extension = g.Extension,
                        FileType = g.FileType,
                        Description = g.Description,
                        UploadedBy = g.UploadedBy,
                        UserFormId = g.UserFormId,
                        Data = g.Data
                    }).ToList(),
                    Comments = g.Comments.Select(g => new CommentDto
                    {
                        ApprovalComment = g.ApprovalComment,
                        ApprovalId = g.ApprovalId,
                        UserFormId = g.UserFormId
                    }).ToList(),
                    formWith = $"{approvalwithName.FirstName} {approvalwithName.LastName}"
                };
                formsDto.Add(formsDTo);
            }
            
            foreach (var form in formsDto)
            {
                switch (form.ApprovalStatus)
                {
                    case ApprovalStatus.InProgress:
                        form.Status = "In Progress";
                        break;
                    case ApprovalStatus.Approved:
                        form.Status = "Approved";
                        break;
                    case ApprovalStatus.Rejected:
                        form.Status = "Rejected";
                        break;
                }

                form.RespoCentreName = (await _centreRepo.GetCentre(form.RespoCentreId)).Name;
                
            }
            return new UserFormsResponseModel
            {
                Data = formsDto,
                Message = "Retrieved",
                Status = true,
            };
        }

        public async Task<UserFormResponseModel> GetUserForm(int formId)
        {
            var form = await _formRepo.GetUserform(formId);
            var user = await _userRepo.GetUser(form.UserId);
            var approval = await _approvalRepo.GetApproval(form.ApprovalId);
            var approvalwithName = await _userRepo.GetUser(approval.UserId);
            if (form == null)
            {
                return new UserFormResponseModel
                {
                    Message = "Form not found",
                    Status = true
                };
            }

            return new UserFormResponseModel
            {
                Message = "Form retrieved successfully",
                Status = true,
                Data = new UserFormDto
                {
                    Name = form.FormType,
                    QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(form.Data),
                    RespoCentreId = form.ResponsibilityCentreId,
                    Id = form.Id,
                    ApprovalStatus = form.ApprovalStatus,
                    Data = form.Data,
                    UplodedDocs = form.UplodedDocs.Select(g => new UploadedDocDto
                    {
                        Name = g.Name,
                        Id = g.Id,
                        Extension = g.Extension,
                        FileType = g.FileType,
                        Description = g.Description,
                        UploadedBy = g.UploadedBy,
                        UserFormId = g.UserFormId,
                        Data = g.Data
                    }).ToList(),
                    Comments = form.Comments.Select(g => new CommentDto
                    {
                        ApprovalComment = g.ApprovalComment,
                        ApprovalId = g.ApprovalId,
                        UserFormId = g.UserFormId
                    }).ToList(),
                    RespoCentreName = (await _centreRepo.GetCentre(form.ResponsibilityCentreId)).Name,
                    formWith = $"{approvalwithName.FirstName} {approvalwithName.LastName}"
                }
            };
        }


        public async Task<UserFormsResponseModel> GetAllFormsByApproval(int userId, IList<int> Ids)
        {
            
            var forms = await _formRepo.GetFormsByApproval(Ids);
            var user = await _userRepo.GetUser(userId);
            var formsDto = forms.Select(g => new UserFormDto
            {
                
                Name = g.FormType,
                QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(g.Data),
                RespoCentreId = g.ResponsibilityCentreId,
                Id = g.Id,
                ApprovalStatus = g.ApprovalStatus,
                Data = g.Data,
                UplodedDocs = g.UplodedDocs.Select(g => new UploadedDocDto
                {
                    Name = g.Name,
                    Id = g.Id,
                    Extension = g.Extension,
                    FileType = g.FileType,
                    Description = g.Description,
                    UploadedBy = g.UploadedBy,
                    UserFormId = g.UserFormId,
                    Data = g.Data
                }).ToList(),
                Comments = g.Comments.Select(g => new CommentDto
                {
                    ApprovalComment = g.ApprovalComment,
                    ApprovalId = g.ApprovalId,
                    UserFormId = g.UserFormId
                }).ToList(),
                
            }).ToList();
            foreach (var form in formsDto)
            {
                switch (form.ApprovalStatus)
                {
                    case ApprovalStatus.InProgress:
                        form.Status = "In Progress";
                        break;
                    case ApprovalStatus.Approved:
                        form.Status = "Approved";
                        break;
                    case ApprovalStatus.Rejected:
                        form.Status = "Rejected";
                        break;
                }

                form.RespoCentreName = (await _centreRepo.GetCentre(form.RespoCentreId)).Name;
                
            }
            return new UserFormsResponseModel
            {
                Data = formsDto,
                Message = "Retrieved",
                Status = true,
            };
        }

        // public async Task<UserFormsResponseModel> GetTotalFormsByApproval(int userId)
        // {
        //     var forms = await _formRepo.GetAllFormsByApproval(userId);
        //     var user = await _userRepo.GetUser(userId);
        //     var formsDto = forms.Select(g => new UserFormDto
        //     {
        //         
        //         Name = g.FormType,
        //         QuestionFieldUserForm = JsonSerializer.Deserialize<List<UserAnswerDto>>(g.Data),
        //         RespoCentreId = g.ResponsibilityCentreId,
        //         Id = g.Id,
        //         ApprovalStatus = g.ApprovalStatus,
        //         Data = g.Data,
        //         UplodedDocs = g.UplodedDocs.Select(g => new UploadedDocDto
        //         {
        //             Name = g.Name,
        //             Id = g.Id,
        //             Extension = g.Extension,
        //             FileType = g.FileType,
        //             Description = g.Description,
        //             UploadedBy = g.UploadedBy,
        //             UserFormId = g.UserFormId,
        //             Data = g.Data
        //         }).ToList(),
        //         Comments = g.Comments.Select(g => new CommentDto
        //         {
        //             ApprovalComment = g.ApprovalComment,
        //             ApprovalId = g.ApprovalId,
        //             UserFormId = g.UserFormId
        //         }).ToList(),
        //         formWith = $"{user.FirstName} {user.LastName}"
        //     }).ToList();
        //     foreach (var form in formsDto)
        //     {
        //         switch (form.ApprovalStatus)
        //         {
        //             case ApprovalStatus.InProgress:
        //                 form.Status = "In Progress";
        //                 break;
        //             case ApprovalStatus.Approved:
        //                 form.Status = "Approved";
        //                 break;
        //             case ApprovalStatus.Rejected:
        //                 form.Status = "Rejected";
        //                 break;
        //         }
        //
        //         form.RespoCentreName = (await _centreRepo.GetCentre(form.RespoCentreId)).Name;
        //         
        //     }
        //     return new UserFormsResponseModel
        //     {
        //         Data = formsDto,
        //         Message = "Retrieved",
        //         Status = true,
        //     };
        // }

        public async Task<string> DeleteForm(int id)
        {
            var form = await _formRepo.Getform(id);
            if (form==null)
            {
                return "Form not found";
            }

            form.IsDeleted = true;
            await _formRepo.UpdateForm(form);
            return "Form deleted successfully";
        }

        public async Task<string> GetFormStatus(int id)
        {
            var form = await _formRepo.GetUserform(id);
            if (form==null)
            {
                return "Form not found";
            }

            return $"Status: {form.ApprovalStatus}";
        }

        public async Task<string> GetFormLocation(int id)
        {
            var form = await _formRepo.GetUserform(id);
            if (form == null)
            {
                return "Form not found";
            }
 
            var approval = await _approvalRepo.GetApproval(form.ApprovalId);
            return $"{approval.ApprovalRole}";
        }
    }
}