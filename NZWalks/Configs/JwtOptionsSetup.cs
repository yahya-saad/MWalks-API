namespace MWalks.API.Configs
{
    public class JwtOptionsSetup(IConfiguration _configuration) : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "Jwt";
        public void Configure(JwtOptions options)
        {
            _configuration
                .GetSection(SectionName)
                .Bind(options);
        }
    }
}

