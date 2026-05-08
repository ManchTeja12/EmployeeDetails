namespace EmployeeManage.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public DateOnly? DOB { get; set; } = new DateOnly();
        public string? Passwordhash { get; set; }
    }
}
