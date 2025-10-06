namespace TaskMgmtSys.Web.Entities
{
    public class UserProject : BaseEntity
    {
        public long UserProjectId { get; set; }
        public long UserId { get; set; }
        public AppUser User { get; set; }

        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
