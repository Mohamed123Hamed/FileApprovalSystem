using FileApprovalSystem.Models;

namespace FileApprovalSystem.Repositories
{
    public interface IFileRepository : IGenericRepository<FileRecord>
    {
        Task<List<FileRecord>> GetAllWithIncludes();
        Task<IQueryable<FileRecord>> GetQueryableAsync();
    }
}
