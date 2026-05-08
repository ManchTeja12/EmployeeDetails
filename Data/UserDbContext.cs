using EmployeeManage.Entities;
namespace EmployeeManage.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> users { get; set; }

    }
}
