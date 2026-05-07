using EmployeeManage.Data;
using EmployeeManage.Dtos;
using EmployeeManage.Models;
using Microsoft.EntityFrameworkCore;

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
                EmployeeDob = dto.EmployeeDob,
                EmployeeSalary = dto.EmployeeSalary
            };

            appDbContext.Employees.Add(employee);

            await appDbContext.SaveChangesAsync();
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
            var employee= await appDbContext.Employees
                .Where( e=> e.EmployeeId==employeeId)
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

        public async Task<bool> UpdateEmployee(EmployeeDto employeeDto,Guid employeeId)
        {
            var employee = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if(employee == null)
            {
                return false;
            }

            if (!String.IsNullOrEmpty(employeeDto.EmployeeName))
            {
                employee.EmployeeName= employeeDto.EmployeeName;
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
