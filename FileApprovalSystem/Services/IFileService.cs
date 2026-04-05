using FileApprovalSystem.Models;
using FileApprovalSystem.ViewModels.Files;

namespace FileApprovalSystem.Services
{
    public interface IFileService
    {
        Task CreateFileAsync(FileRecord file);
        Task<(List<FileRecord>, int)> GetPagedAsync(int page, int pageSize);
        Task<List<FileRecord>> SearchAsync(FileSearchViewModel model);
        Task ApproveAsync(int fileId, int employeeId);
        Task CreateFileAsync(CreateFileViewModel model, int userId);
        Task<object> GetStatsAsync();
    }
}
