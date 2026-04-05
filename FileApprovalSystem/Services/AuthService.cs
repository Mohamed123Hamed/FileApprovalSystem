using FileApprovalSystem.Models;
using FileApprovalSystem.Repositories;

namespace FileApprovalSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepo;

        public AuthService(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public async Task<Employee?> LoginAsync(string username, string password)
        {
            return await _employeeRepo
                .GetByUsernameAndPasswordAsync(username, password);
        }
    }
}
