namespace Authentication.Infrastructure.Identity.Services
{
    using Authentication.Infrastructure.Identity.Models;
    using Authentication.Infrastructure.Identity.Models.Authentication;
    using Authentication.Infrastructure.Identity.Services.Response;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class TokenService : ITokenService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Token _token;
        private readonly HttpContext _httpContext;

        public TokenService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<Token> tokenOptions,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = tokenOptions.Value;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<ServiceResponse<TokenResponse>> Authenticate(TokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.Username, request.Password))
            {
                ApplicationUser user = await GetUserByEmail(request.Username);

                if (user != null && user.IsEnabled)
                {
                    string role = (await _userManager.GetRolesAsync(user))[0];
                    string jwtToken = await GenerateJwtToken(user);

                    await _userManager.UpdateAsync(user);

                    var response = new TokenResponse(user, role, jwtToken);

                    return new ServiceResponse<TokenResponse>(response);
                }
            }

            return new ServiceResponse<TokenResponse>("User is not valid!"); ;
        }

        private async Task<bool> IsValidUser(string username, string password)
        {
            ApplicationUser user = await GetUserByEmail(username);

            if (user == null)
            {
                return false;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            string role = (await _userManager.GetRolesAsync(user))[0];
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim("FullName", $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}

