using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage="Name is Requried")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]

        public string? UserEmail { get; set; }

        public DateOnly? DOB { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
