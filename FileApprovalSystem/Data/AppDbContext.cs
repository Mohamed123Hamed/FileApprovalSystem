using FileApprovalSystem.Enums;
using FileApprovalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FileApprovalSystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FileRecord> Files { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Approval> Approvals { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

                new Employee
                {
                    Id = 1,
                    Name = "Employee 1",
                    Username = "emp1",
                    Password = "123",
                    Role = EmployeeRole.Employee1
                },

                new Employee
                {
                    Id = 2,
                    Name = "Employee 2",
                    Username = "emp2",
                    Password = "123",
                    Role = EmployeeRole.Employee2
                },

                new Employee
                {
                    Id = 3,
                    Name = "Employee 3",
                    Username = "emp3",
                    Password = "123",
                    Role = EmployeeRole.Employee3
                }

                );


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "HR" },
                new Category { Id = 2, Name = "Finance" },
                new Category { Id = 3, Name = "IT" }
            );
        }
    }
}
