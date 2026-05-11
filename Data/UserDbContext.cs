using EmployeeManage.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManage.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> users { get; set; }
    public DbSet<Employee> employees { get; set; }

  }
}
