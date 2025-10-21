namespace TaskMgmtSys.Web.Models
{
    public class Project
    {
        public long? Id { get; set; }
        public required string ProjectName { get; set; }
        public string? Description { get; set; }
    }
}
