using System;
using System.Collections.Generic;
using MEMOJET.Contract;
using MEMOJET.Identity;

namespace MEMOJET.Entities
{
    public class  UserForm:AuditableEntity
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public Form Form { get; set; }
        public User User { get; set; }
        public string FormType { get; set; }
        public string Data { get; set; }
        public  int ApprovalId { get; set; } //Id of first approval to be hit by form
        public int ResponsibilityCentreId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public ApprovalAction ApprovalAction { get; set; }
        public DateTime ArrivedApproval { get; set; }
         public IList<UploadedDoc> UplodedDocs { get; set; }= new List<UploadedDoc>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
         }
}