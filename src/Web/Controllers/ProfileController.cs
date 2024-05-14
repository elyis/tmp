using System.Net;
using tmp.src.Domain.Entities.Response;
using tmp.src.Domain.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using webApiTemplate.src.App.IService;
using tmp.src.Domain.Entities.Request;
using System.Net.Http.Headers;

namespace tmp.src.Web.Controllers
{
    [ApiController]
    [Route("tmp")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public ProfileController(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }


        [HttpGet("profile"), Authorize]
        [SwaggerOperation("Получить профиль")]
        [SwaggerResponse(200, Description = "Успешно", Type = typeof(ProfileBody))]
        public async Task<IActionResult> GetProfileAsync(
            [FromHeader(Name = nameof(HttpRequestHeader.Authorization))] string token
        )
        {
            var tokenInfo = _jwtService.GetTokenInfo(token);
            var user = await _userRepository.GetAsync(tokenInfo.UserId);
            return user == null ? NotFound() : Ok(user.ToProfileBody());
        }

        [HttpPut("profile"), Authorize]
        [SwaggerOperation("Обновить профиль")]
        [SwaggerResponse(200)]

        public async Task<IActionResult> UpdateProfileAsync(
            UpdateProfileBody body,
            [FromHeader(Name = nameof(HttpRequestHeaders.Authorization))] string token)

        {
            var jwtPayload = _jwtService.GetTokenInfo(token);
            var result = await _userRepository.UpdateProfile(body, jwtPayload.UserId);
            return result == null ? BadRequest() : Ok();
        }
    }
}