namespace TaskMgmtSys.Web.Entities
{
    public class Project : BaseEntity
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        //public ICollection<UserProject>? UserProjects { get; set; }
        //public ICollection<TaskItem>? TaskItems { get; set; }
    }
}
