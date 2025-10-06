namespace TaskMgmtSys.Web.Entities
{
    public class AttachmentBase : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
    }
}
