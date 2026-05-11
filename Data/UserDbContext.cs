using Microsoft.EntityFrameworkCore;
using EmployeeManage.Entities;
namespace EmployeeManage.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) :DbContext(options)
    {
        public DbSet<User> users { get; set; }
    public DbSet<Employee> employees { get; set; }

  }
}
