using Domain;
using HR.Models;
using Microsoft.EntityFrameworkCore;

namespace HR.Contexts
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SalaryTier> SalaryTiers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "HR", InActive = true },
                new Department { DepartmentId = 2, DepartmentName = "IT", InActive = true },
                new Department { DepartmentId = 3, DepartmentName = "Finance", InActive = true }
            );

            modelBuilder.Entity<SalaryTier>().HasData(
                new SalaryTier { SalaryTierId = 1, TierName = "Junior", SalaryAmount = 3000, InActive = true },
                new SalaryTier { SalaryTierId = 2, TierName = "Mid-Level", SalaryAmount = 5000, InActive = true },
                new SalaryTier { SalaryTierId = 3, TierName = "Senior", SalaryAmount = 7000, InActive = true }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Alice Smith", Position = "HR Manager", InActive = true, DepartmentId = 1, SalaryTierId = 2 },
                new Employee { EmployeeId = 2, Name = "Bob Johnson", Position = "Software Developer", InActive = true, DepartmentId = 2, SalaryTierId = 1 },
                new Employee { EmployeeId = 3, Name = "Charlie Brown", Position = "Finance Analyst", InActive = true, DepartmentId = 3, SalaryTierId = 2 },
                new Employee { EmployeeId = 4, Name = "Diana Prince", Position = "Senior Developer", InActive = true, DepartmentId = 2, SalaryTierId = 3 }
            );
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Email = "mashal200315@gmail.om", PasswordHash = "123", UserName = "ibo", FirstName = "ibrahim", LastName = "mashal" }
                
            );
        }

    }
}
