namespace TaskMgmtSys.Web.Entities
{
    public class Comment : BaseEntity
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public long TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public long UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<CommentAttachment>? CommentAttachments { get; set; }
    }
}
