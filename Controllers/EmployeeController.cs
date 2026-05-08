using EmployeeManage.Dtos;
using EmployeeManage.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class EmployeeController : Controller
  {

    private readonly EmployeeService employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
      this.employeeService = employeeService;
    }

        //post employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeApi([FromBody] EmployeeDto employeeDto)
        {

            try
            {
                await employeeService.AddEmployee(employeeDto);

                ApiResponse apiResponse = new ApiResponse
                {
                    success = true,
                    message = "employee created"
                };
                return Ok(apiResponse);


            }
            catch (Exception ex)

            {
                ApiResponse apiResponse = new ApiResponse
                {
                    success = false,
                    message = ex.Message
                };
                return BadRequest(apiResponse);
            }
        }
    // get all employees
    [HttpGet("employees")]
    public async Task<IActionResult> GetAllEmployeesApi()
    {
      List<EmployeeDto> employees = await employeeService.GetAllEmployees();

      if (employees == null || employees.Count == 0)
      {
        return NotFound();
      }

      return Ok(employees);
    }

    //get employee by id

    [HttpGet("{employeeId}")]

    public async Task<IActionResult> GetEmployeeByIdApi(Guid employeeId)
    {
      var employee = await employeeService.GetEmployeeById(employeeId);
      if (employee == null)
      {
        return NotFound("employee not found");
      }
      return Ok(employee);
    }

    //update employee by id

    [HttpPatch("update/{employeeId}")]
    public async Task<IActionResult> UpdateEmployeeApi(EmployeeDto employeeDto, Guid employeeId)
    {
      var UpdatedEmployee = await employeeService.UpdateEmployee(employeeDto, employeeId);
      if (UpdatedEmployee == null)
      {
        return NotFound("employee not found");
      }
      return Ok("employee updated");
    }

    [HttpDelete("delete/{employeeId}")]

    public async Task<IActionResult> DeleteEmployeeApi(Guid employeeId)
    {
      var deleted = await employeeService.DeleteEmployee(employeeId);

      if (!deleted)
      {
        return NotFound("Employee not found");
      }

      return Ok("Employee deleted");
    }
  }
}
