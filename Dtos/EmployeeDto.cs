namespace EmployeeManage.Dtos
{
  public class EmployeeDto
  {
    public Guid EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public string? EmployeeEmail { get; set; }

    public DateTime EmployeeDob { get; set; }

    public decimal EmployeeSalary { get; set; }
  }
}
