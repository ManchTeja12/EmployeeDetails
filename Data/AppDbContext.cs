using EmployeeManage.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManage.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {

        public DbSet<Employee> Employees { get; set; }
    }

    
}
