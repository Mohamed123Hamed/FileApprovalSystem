using FileApprovalSystem.Models;

namespace FileApprovalSystem.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee?> GetByUsernameAndPasswordAsync(string username, string password);
    }
}
