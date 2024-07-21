namespace MWalks.API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthRepository(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IJwtService jwtService = null)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }



        public async Task Register(RegisterDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username,
            };

            if (dto.Roles == null || !dto.Roles.Any())
                throw new BadRequestException("The roles field is required and should not be empty.");

            if (!await RolesExistAsync(dto.Roles))
                throw new BadRequestException("One or more roles do not exist.", errors: ["One or more roles do not exist"]);

            var result = await _userManager.CreateAsync(identityUser, dto.Password);

            if (!result.Succeeded)
                throw new BadRequestException("Failed to register", result.Errors.Select(e => e.Description).ToArray());

            var roleResult = await _userManager.AddToRolesAsync(identityUser, dto.Roles);
            if (!roleResult.Succeeded)
                throw new BadRequestException("Failed to assign roles", roleResult.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedException("Invalid username or password");

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtService.GenerateJwtToken(user, roles.ToList());

            return accessToken;
        }

        private async Task<bool> RolesExistAsync(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
