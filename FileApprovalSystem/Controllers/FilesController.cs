using FileApprovalSystem.Repositories;
using FileApprovalSystem.Services;
using FileApprovalSystem.ViewModels.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Controllers
{
    public class FilesController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;

        public FilesController(ICategoryRepository categoryRepo,
                               IFileService fileService,IFileRepository fileRepository)
        {
            _categoryRepo = categoryRepo;
            _fileService = fileService;
            _fileRepository = fileRepository;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }



        public async Task<IActionResult> Index(FileSearchViewModel search, int page = 1)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            int pageSize = 5;

            var query = await _fileRepository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(search.FileNumber))
                query = query.Where(x => x.FileNumber.Contains(search.FileNumber));

            if (!string.IsNullOrWhiteSpace(search.Subject))
                query = query.Where(x => x.Subject.Contains(search.Subject));

            if (search.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == search.CategoryId);

            if (search.Status.HasValue)
                query = query.Where(x => x.Status == search.Status);

            var totalCount = await query.CountAsync();

            var files = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ViewBag.Categories = await _categoryRepo.GetAllAsync();

            return View(files);
        }

        public async Task<IActionResult> Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewBag.Categories = await _categoryRepo.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryRepo.GetAllAsync();
                return View(model);
            }

            await _fileService.CreateFileAsync(model, userId.Value);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Approve(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            try
            {
                await _fileService.ApproveAsync(id, userId.Value);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            var stats = await _fileService.GetStatsAsync();
            return View(stats);
        }
    }
}
