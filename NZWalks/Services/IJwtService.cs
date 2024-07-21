namespace MWalks.API.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user, List<string> roles);
    }
}
