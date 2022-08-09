namespace MEMOJET.DTOs
{
    public class UploadedDocDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public int UploadedBy { get; set; }
        public int UserFormId { get; set; }
    }
    
    public class UploadedDocResponseModel:BaseResponse
    {
        public UploadedDocDto Data { get; set; }
    }
}