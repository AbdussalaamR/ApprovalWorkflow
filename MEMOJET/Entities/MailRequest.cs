using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MEMOJET.Entities
{
    public class MailRequest
    {
        public string ToEmail{ get; set; }
        public string ToName { get; set; }
        //public byte [] Content { get; set; }
        
        
        public Dictionary<string,byte []> AttachedDocs { get; set; }
        public string HtmlContent { get; set; }
        public string Subject { get; set; }
        //public ICollection<IFormFile> Attachment { get; set; }
    }
}