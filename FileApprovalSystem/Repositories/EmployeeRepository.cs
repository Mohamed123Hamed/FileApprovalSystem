using FileApprovalSystem.Data;
using FileApprovalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
