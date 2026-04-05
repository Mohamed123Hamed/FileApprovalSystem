using FileApprovalSystem.Enums;

namespace FileApprovalSystem.ViewModels.Files
{
    public class FileSearchViewModel
    {
        public string? FileNumber { get; set; }
        public string? Subject { get; set; }
        public int? CategoryId { get; set; }
        public FileStatus? Status { get; set; }
    }
}
