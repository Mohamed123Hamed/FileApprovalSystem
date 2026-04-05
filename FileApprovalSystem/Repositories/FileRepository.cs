using FileApprovalSystem.Data;
using FileApprovalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Repositories
{
    public class FileRepository : GenericRepository<FileRecord>, IFileRepository
    {
        private readonly AppDbContext _context;

        public FileRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FileRecord>> GetAllWithIncludes()
        {
            return await _context.Files
                .Include(x => x.SubmittedBy)
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<IQueryable<FileRecord>> GetQueryableAsync()
        {
            return _context.Files
                .Include(x => x.Category)
                .AsQueryable();
        }

    }
}
