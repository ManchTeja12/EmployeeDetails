using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}