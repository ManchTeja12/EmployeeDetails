using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}