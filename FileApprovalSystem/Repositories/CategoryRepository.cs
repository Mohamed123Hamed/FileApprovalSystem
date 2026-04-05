using FileApprovalSystem.Data;
using FileApprovalSystem.Models;

namespace FileApprovalSystem.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
