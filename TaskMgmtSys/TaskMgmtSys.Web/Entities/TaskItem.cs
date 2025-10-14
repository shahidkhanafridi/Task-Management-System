namespace TaskMgmtSys.Web.Entities
{
    public enum TaskItemStatus : int
    {
        NotStarted = 0,
        InProgress = 1,
        PendingMerging = 2,
        QAQC = 3,
        Completed = 4
    }

    public class TaskItem : BaseEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public TaskItemStatus StatusId { get; set; } = TaskItemStatus.NotStarted;
        public DateTime? DueDate { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public long? ActiveAssignedUserId { get; set; }
        public AppUser? ActiveAssignedUser { get; set; }

        //public ICollection<TaskAssignment>? TaskAssignments { get; set; }
        //public ICollection<TaskAttachment>? TaskAttachments { get; set; }
        //public ICollection<Comment>? Comments { get; set; }
    }
}
