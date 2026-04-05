using FileApprovalSystem.Data;
using FileApprovalSystem.Repositories;
using FileApprovalSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
