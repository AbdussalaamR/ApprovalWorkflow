namespace MEMOJET.Entities
{
    public class UploadedDoc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public int UploadedBy { get; set; }
        public int UserFormId { get; set; }
        public UserForm UserForm { get; set; }
        
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}