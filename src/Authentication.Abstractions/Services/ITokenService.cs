namespace Authentication.Abstractions.Services
{
    using Authentication.DataTransferModels.Authentication;
    using Authentication.DataTransferModels.Response;

    public interface ITokenService
    {
        Task<ServiceResponse<TokenResponse>> Authenticate(TokenRequest request, CancellationToken cancellationToken);
    }
}

