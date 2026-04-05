namespace FileApprovalSystem.Models
{
    public class Approval
    {
        public int Id { get; set; }

        public int FileRecordId { get; set; }

        public int EmployeeId { get; set; }

        public int Order { get; set; }

        public bool IsApproved { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
