using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId {  get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        public string EmployeeEmail { get; set; }
        [Required]
        public DateTime EmployeeDob { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
    }
}
