using FileApprovalSystem.Enums;

namespace FileApprovalSystem.Helpers
{
    public static class StatusHelper
    {
        public static string GetStatusClass(FileStatus status)
        {
            return status switch
            {
                FileStatus.PendingEmployee2 => "bg-warning",
                FileStatus.PendingEmployee3 => "bg-info",
                FileStatus.Approved => "bg-success",
                _ => "bg-secondary"
            };
        }
    }
}
