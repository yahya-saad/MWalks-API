namespace MWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthRepository _authRepository) : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _authRepository.Register(dto);
            return Ok("User registered successfully! Please login");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var accessToken = await _authRepository.Login(dto);
            return Ok(new { accessToken });
        }
    }
}