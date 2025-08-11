using InternetShopApi.Contracts.Dtos.AuthDto;
using InternetShopApi.Domain.Entities.Auth;
using InternetShopApi.Service.Service.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController (IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("register")]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegistrationAsync(dto);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok("User created successfully");
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(token);
        }


    }
}
