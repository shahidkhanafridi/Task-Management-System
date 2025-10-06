namespace TaskMgmtSys.Web.Entities
{
    public class Comment : BaseEntity
    {
        public long CommentId { get; set; }
        public string Content { get; set; }

        //public long TaskItemId { get; set; }
        //public TaskItem TaskItems { get; set; }

        //public long UserId { get; set; }
        //public AppUser User { get; set; }

        // Navigation
        //public ICollection<CommentAttachment>? CommentAttachments { get; set; }
    }
}
