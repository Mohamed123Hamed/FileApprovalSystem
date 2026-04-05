using FileApprovalSystem.Enums;

namespace FileApprovalSystem.Models
{
    public class FileRecord
    {
        public int Id { get; set; }
        public string FileNumber { get; set; }
        public string Subject { get; set; }
        public int SubmittedById { get; set; }
        public Employee SubmittedBy { get; set; }
        public FileStatus Status { get; set; }
        public DateTime FileDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ResponsibleEmployee { get; set; }
        public string AttachmentPath { get; set; }
        public List<Approval> Approvals { get; set; }
    }
}
