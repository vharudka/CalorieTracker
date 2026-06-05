using CalorieTracker.Api.Dtos.Auths;
using CalorieTracker.Api.Services.Auths;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        private readonly IValidator<RegisterRequest> _registerValidator;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly ILogger<AuthController> _logger;

        public AuthController
        (
            IAuthService auth,
            IValidator<RegisterRequest> registerValidator,
            IValidator<LoginRequest> loginValidator,
            ILogger<AuthController> logger
        )
        {
            _auth = auth;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            _logger.LogInformation("Register request received for username {Username}", request.Username);

            var validation = await _registerValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                _logger.LogWarning("Register validation failed for username {Username}: {@Errors}", request.Username, validation.Errors);
                return BadRequest(validation.Errors);
            }

            var result = await _auth.RegisterAsync(request);

            _logger.LogInformation("User {Username} registered successfully", request.Username);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            _logger.LogInformation("Login request received for username {Username}", request.Username);

            var validation = await _loginValidator.ValidateAsync(request);
            if (!validation.IsValid)
            {
                _logger.LogWarning("Login validation failed for username {Username}: {@Errors}", request.Username, validation.Errors);
                return BadRequest(validation.Errors);
            }

            var result = await _auth.LoginAsync(request);

            _logger.LogInformation("User {Username} logged in successfully", request.Username);

            return Ok(result);
        }
    }
}