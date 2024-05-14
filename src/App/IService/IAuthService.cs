using tmp.src.Domain.Entities.Request;
using Microsoft.AspNetCore.Mvc;

namespace tmp.src.App.IService
{
    public interface IAuthService
    {
        Task<IActionResult> SignUp(SignUpBody body, string rolename);
        Task<IActionResult> SignIn(SignInBody body);
        Task<IActionResult> RestoreToken(string refreshToken);
    }
}