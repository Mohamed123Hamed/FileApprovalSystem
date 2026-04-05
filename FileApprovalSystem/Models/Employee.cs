using FileApprovalSystem.Enums;

namespace FileApprovalSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public EmployeeRole Role { get; set; }
    }
}
