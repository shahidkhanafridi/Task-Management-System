namespace TaskMgmtSys.Web.Entities
{
    public class Project : BaseEntity
    {
        public long Id { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }

        public ICollection<UserProject>? UserProjects { get; set; }
        public ICollection<TaskItem>? TaskItems { get; set; }
    }
}
