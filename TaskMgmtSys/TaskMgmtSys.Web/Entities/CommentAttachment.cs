namespace TaskMgmtSys.Web.Entities
{
    public class CommentAttachment : AttachmentBase
    {
        public long Id { get; set; }
        public long CommentId { get; set; }
        public Comment Comment { get; set; }
        public long UserId { get; set; }
        public AppUser User { get; set; }
    }
}
