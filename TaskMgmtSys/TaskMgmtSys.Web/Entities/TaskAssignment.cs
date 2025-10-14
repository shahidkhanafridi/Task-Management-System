namespace TaskMgmtSys.Web.Entities
{
    public class TaskAssignment : BaseEntity
    {
        public long Id { get; set; }

        public long TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; }

        public long UserId { get; set; }
        public AppUser User { get; set; }
    }
}
