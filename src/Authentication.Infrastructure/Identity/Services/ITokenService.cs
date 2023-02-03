namespace Authentication.Infrastructure.Identity.Services
{
    using Authentication.Infrastructure.Identity.Models.Authentication;
    using Authentication.Infrastructure.Identity.Services.Response;

    public interface ITokenService
    {
        Task<ServiceResponse<TokenResponse>> Authenticate(TokenRequest request, string ipAddress);
    }
}

