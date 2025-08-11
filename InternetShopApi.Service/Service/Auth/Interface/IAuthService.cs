using InternetShopApi.Contracts.Dtos.AuthDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace InternetShopApi.Service.Service.Auth.Interface
{
    public interface IAuthService
    {
        Task<IdentityResult> RegistrationAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
