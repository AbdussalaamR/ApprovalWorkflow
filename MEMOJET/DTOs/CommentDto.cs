namespace MEMOJET.DTOs
{
    public class CommentDto
    {
        public int UserFormId { get; set; }
        public int Id { get; set; }
        public int ApprovalId { get; set; }
        public string ApprovalComment { get; set; }
    }
}