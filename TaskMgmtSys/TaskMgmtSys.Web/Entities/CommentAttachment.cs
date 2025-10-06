namespace TaskMgmtSys.Web.Entities
{
    public class CommentAttachment : AttachmentBase
    {
        public long CommentAttachmentId { get; set; }
        public long CommentId { get; set; }
        //public Comment Comment { get; set; }
    }
}
