namespace MWalks.API.DTOs
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
