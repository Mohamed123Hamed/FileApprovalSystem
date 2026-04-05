using FileApprovalSystem.Models;

namespace FileApprovalSystem.Services
{
    public interface IAuthService
    {
        Task<Employee?> LoginAsync(string username, string password);

    }
}
