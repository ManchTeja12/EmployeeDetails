namespace EmployeeManage.Dtos
{
  public class UserResponse
  {
    public Guid Id { get; set; } 
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public DateOnly? DOB { get; set; }
    public string Token { get; set; }
  }
}
