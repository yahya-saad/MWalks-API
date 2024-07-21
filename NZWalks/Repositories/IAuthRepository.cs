namespace MWalks.API.Repositories
{
    public interface IAuthRepository
    {
        Task Register(RegisterDto dto);
        Task<string> Login(LoginDto dto);
    }
}
