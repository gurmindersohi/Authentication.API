namespace Authentication.API.Controllers
{
    using Authentication.Abstractions.Services;
    using Authentication.API.Extensions;
    using Authentication.DataTransferModels.Authentication;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Net.Mime;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public TokenController(
            ITokenService tokenService,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost(Name = nameof(Authenticate))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<IActionResult> Authenticate(
            [FromBody][Required] TokenRequest tokenRequest,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var response = await _tokenService.Authenticate(tokenRequest, cancellationToken);

            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Data);
        }
    }
}

