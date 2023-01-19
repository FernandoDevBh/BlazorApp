using System.Net.Http.Headers;
using Client.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorageService;
    private readonly AuthenticationStateProvider authStateProvider;

    public AuthenticationService(HttpClient httpClient,
                                 ILocalStorageService localStorageService,
                                 AuthenticationStateProvider authStateProvider)
    {
        this.httpClient = httpClient;
        this.localStorageService = localStorageService;
        this.authStateProvider = authStateProvider;
    }

    public async Task<SignInResponseDTO> Login(SignInRequestDTO requestDTO)
    {
        var bodyContent = requestDTO.GetStringContent();
        var response = await httpClient.PostAsync("account/signin", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<SignInResponseDTO>();
        if(response.IsSuccessStatusCode)
        {
            await localStorageService.SetItemAsync(SD.Local_Token, responseDTO.Token);
            await localStorageService.SetItemAsync(SD.Local_UserDetails, responseDTO.UserDTO);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SD.Token_Type, responseDTO.Token);
            authStateProvider.NotifyUserLoggedIn(responseDTO.Token ?? string.Empty);
            return new SignInResponseDTO
            {
                IsAuthSuccessful = true,
            };

        }
        else
        {
            return responseDTO;
        }
    }

    public async Task Logout()
    {
        await localStorageService.RemoveItemAsync(SD.Local_Token);
        await localStorageService.RemoveItemAsync(SD.Local_UserDetails);
        authStateProvider.NotifyUserLoggedOut();
        httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO requestDTO)
    {
        var bodyContent = requestDTO.GetStringContent();
        var response = await httpClient.PostAsync("account/signup", bodyContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var responseDTO = responseContent.DeserializeObject<SignUpResponseDTO>();

        if (response.IsSuccessStatusCode)
        {
            return new SignUpResponseDTO { IsRegisterationSuccessful = true, };
        }
        else
        {
            return new SignUpResponseDTO { IsRegisterationSuccessful = false, Errors = responseDTO.Errors };
        }
    }
}
