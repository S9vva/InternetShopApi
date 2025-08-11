
using InternetShopApi.Contracts.Dtos.AuthDto;
using InternetShopApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InternetShopApi.Domain.Entities;
using InternetShopApi.Service.Service.Interfaces;


namespace InternetShopApi.Service.Service.Auth.Interface
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration , ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _customerService = customerService;
        }

        public async Task<IdentityResult> RegistrationAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed{errors}");
            }

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            var userCreate = new Customer
            {
                CustomerId = user.Id,
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.FirstName,
                SurName = dto.LastName
            };

            await _customerService.CreateCustomerAsync(userCreate);

            return result;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UsernName);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid credentials");

            return await GenerateJwtToken(user);
        }

        private async Task<AuthResponseDto> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(3);

            var token = new JwtSecurityToken(
           issuer: _configuration["JWTKey:ValidIssuer"],
           audience: _configuration["JWTKey:ValidAudience"],
           claims: claims,
           expires: expires,
           signingCredentials: creds
        );

            return await Task.FromResult(new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            });
        }

    }
}
