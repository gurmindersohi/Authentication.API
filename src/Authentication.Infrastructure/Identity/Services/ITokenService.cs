namespace Authentication.Infrastructure.Identity.Services
{
    using Authentication.Infrastructure.Identity.Models.Authentication;

    /// <summary>
    ///     A collection of token related services
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        ///     Validate the credentials entered when logging in.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);
    }
}

