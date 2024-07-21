namespace MWalks.API.DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
