using Models;

namespace Client.Services.Contracts;

public interface IAuthenticationService
{
    Task<SignInResponseDTO> Login(SignInRequestDTO requestDTO);
    Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO);
    Task Logout();
}
