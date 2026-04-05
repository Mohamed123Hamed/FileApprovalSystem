using FileApprovalSystem.Enums;
using FileApprovalSystem.Models;
using FileApprovalSystem.Repositories;
using FileApprovalSystem.ViewModels.Files;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepo;
        private readonly IGenericRepository<Employee> _empRepo;

        public FileService(IFileRepository fileRepo,
                           IGenericRepository<Employee> empRepo)
        {
            _fileRepo = fileRepo;
            _empRepo = empRepo;
        }

        public async Task CreateFileAsync(FileRecord file)
        {
            file.Status = FileStatus.PendingEmployee2;

            await _fileRepo.AddAsync(file);
            await _fileRepo.SaveAsync();
        }

        public async Task<(List<FileRecord>, int)> GetPagedAsync(int page, int pageSize)
        {
            var query = await _fileRepo.GetQueryableAsync();

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task ApproveAsync(int fileId, int employeeId)
        {
            var file = await _fileRepo.GetByIdAsync(fileId);
            var emp = await _empRepo.GetByIdAsync(employeeId);

            if (file == null || emp == null)
                throw new Exception("Invalid Data");

            if (emp.Role == EmployeeRole.Employee2 &&
                file.Status == FileStatus.PendingEmployee2)
            {
                file.Status = FileStatus.PendingEmployee3;
            }
            else if (emp.Role == EmployeeRole.Employee3 &&
                     file.Status == FileStatus.PendingEmployee3)
            {
                file.Status = FileStatus.Approved;
            }
            else
            {
                throw new Exception("Not Allowed");
            }

            _fileRepo.Update(file);
            await _fileRepo.SaveAsync();
        }



        public async Task CreateFileAsync(CreateFileViewModel model, int userId)
        {
            var file = new FileRecord
            {
                FileNumber = model.FileNumber,
                Subject = model.Subject,
                CategoryId = model.CategoryId,
                FileDate = model.FileDate,
                ResponsibleEmployee = model.ResponsibleEmployee,
                SubmittedById = userId,
                Status = FileStatus.PendingEmployee2
            };

            if (model.Attachment != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.Attachment.FileName);
                var path = Path.Combine("wwwroot/uploads", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await model.Attachment.CopyToAsync(stream);

                file.AttachmentPath = fileName;
            }

            await _fileRepo.AddAsync(file);
            await _fileRepo.SaveAsync();
        }


        public async Task<List<FileRecord>> SearchAsync(FileSearchViewModel model)
        {
            var query = await _fileRepo.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(model.FileNumber))
                query = query.Where(x => x.FileNumber.Contains(model.FileNumber));

            if (!string.IsNullOrWhiteSpace(model.Subject))
                query = query.Where(x => x.Subject.Contains(model.Subject));

            if (model.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == model.CategoryId.Value);

            if (model.Status.HasValue)
                query = query.Where(x => x.Status == model.Status.Value);

            return await query.ToListAsync();
        }




        public async Task<object> GetStatsAsync()
        {
            var query = await _fileRepo.GetQueryableAsync();

            return new
            {
                Total = await query.CountAsync(),
                PendingE2 = await query.CountAsync(x => x.Status == FileStatus.PendingEmployee2),
                PendingE3 = await query.CountAsync(x => x.Status == FileStatus.PendingEmployee3),
                Approved = await query.CountAsync(x => x.Status == FileStatus.Approved)
            };
        }
    }
}
