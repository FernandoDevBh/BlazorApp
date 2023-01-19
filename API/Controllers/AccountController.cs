using Models;
using Common;
using API.Helper;
using DataAccess;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly APISettings _apiSettings;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IOptions<APISettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _apiSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDTO)
        {
            if (signUpRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            ApplicationUser user = new()
            {
                UserName = signUpRequestDTO.Email,
                Email = signUpRequestDTO.Email,
                Name = signUpRequestDTO.Name,
                PhoneNumber = signUpRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, signUpRequestDTO.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(e => e.Description).ToArray()
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);

            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO
                {
                    IsRegisterationSuccessful = false,
                    Errors = roleResult.Errors.Select(e => e.Description).ToArray()
                });
            }

            return Created("", new SignUpResponseDTO
            {
                IsRegisterationSuccessful = true,
            });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
            if (user == null)
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }

            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
                    issuer: _apiSettings.ValidIssuer,
                    audience: _apiSettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new SignInResponseDTO
            {
                Token = token,
                IsAuthSuccessful = true,
                UserDTO = new()
                {
                    Email = user.Email ?? string.Empty,
                    Id = user.Id ?? string.Empty,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber ?? string.Empty
                }
            });
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(_apiSettings.GetSecretKey());

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email?? string.Empty),
                new Claim("Id", user.Id),
            };
            if(!string.IsNullOrEmpty(user.Email))
            {
                var currentUser = await _userManager.FindByEmailAsync(user.Email);
                if(currentUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(currentUser);
                    foreach (var role in roles)
                        claims.Add(new(ClaimTypes.Role, role));
                }
            }

            return claims;
        }
    }
}
