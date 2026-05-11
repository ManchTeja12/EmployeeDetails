using EmployeeManage.Dtos;

namespace EmployeeManage.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto user);
        Task<UserResponse> Login(UserDto user);
    }
}
