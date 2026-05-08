using EmployeeManage.Data;
using EmployeeManage.Dtos;
using EmployeeManage.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace EmployeeManage.Services
{
  public class EmployeeService
  {

    private readonly AppDbContext appDbContext;
    public EmployeeService(AppDbContext appDbContext)
    {
      this.appDbContext = appDbContext;
    }


    //add employee

    public async Task AddEmployee(EmployeeDto dto)
    {
      var employee = new Employee()
      {
        EmployeeId = Guid.NewGuid(),
        EmployeeName = dto.EmployeeName,
        EmployeeEmail = dto.EmployeeEmail,
        EmployeeDob = DateTime.SpecifyKind(dto.EmployeeDob, DateTimeKind.Utc),
        EmployeeSalary = dto.EmployeeSalary
      };
      bool exist = await this.appDbContext.Employees.AnyAsync(x => x.EmployeeEmail == dto.EmployeeEmail);
      if (employee.EmployeeEmail == null)
      {
        throw new Exception("email required");
      }
      if (employee.EmployeeName == null)
      {
        throw new Exception("name required");
      }

      if (string.IsNullOrWhiteSpace(employee.EmployeeName))
      {
        throw new Exception("username cant be null");
      }
      if (exist)
      {
        throw new Exception("user already exists");
      }

      if (!IsValidEmail(employee.EmployeeEmail))
      {
        throw new Exception("Invalid email format");
      }
      if (employee.EmployeeSalary < 0)
      {
        throw new Exception("salary should not be less than 0");
      }



      appDbContext.Employees.Add(employee);

      await appDbContext.SaveChangesAsync();
    }

    private bool IsValidEmail(string employeeEmail)
    {
      try
      {
        var mail = new MailAddress(employeeEmail);
        return true;
      }
      catch
      {
        return false;
      }
    }

    //get all employees

    public async Task<List<EmployeeDto>> GetAllEmployees()
    {
      var employees = await appDbContext.Employees
          .Select(e => new EmployeeDto
          {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.EmployeeName,
            EmployeeEmail = e.EmployeeEmail,
            EmployeeDob = e.EmployeeDob,
            EmployeeSalary = e.EmployeeSalary
          })
          .ToListAsync();

      return employees;
    }

    //get employee by id

    public async Task<EmployeeDto> GetEmployeeById(Guid employeeId)
    {
      var employee = await appDbContext.Employees
          .Where(e => e.EmployeeId == employeeId)
          .Select(e => new EmployeeDto
          {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.EmployeeName,
            EmployeeEmail = e.EmployeeEmail,
            EmployeeDob = e.EmployeeDob,
            EmployeeSalary = e.EmployeeSalary
          })
          .FirstOrDefaultAsync();

      return employee;
    }

    // update employee

    public async Task<bool> UpdateEmployee(EmployeeDto employeeDto, Guid employeeId)
    {
      var employee = await appDbContext.Employees
          .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
      if (employee == null)
      {
        return false;
      }

      if (!String.IsNullOrEmpty(employeeDto.EmployeeName))
      {
        employee.EmployeeName = employeeDto.EmployeeName;
      }
      if (!String.IsNullOrEmpty(employeeDto.EmployeeEmail))
      {
        employee.EmployeeEmail = employeeDto.EmployeeEmail;
      }
      await appDbContext.SaveChangesAsync();
      return true;
    }



    // delete employee by id
    public async Task<bool> DeleteEmployee(Guid employeeId)
    {
      var employee = await appDbContext.Employees
          .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
      if (employee == null)
      {
        return false;
      }

      appDbContext.Employees.Remove(employee);
      await appDbContext.SaveChangesAsync();
      return true;

    }

  }
}
