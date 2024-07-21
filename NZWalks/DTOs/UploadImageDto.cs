namespace MWalks.API.DTOs
{
    public class UploadImageDto
    {
        [FileValidator("jpg,jpeg,png", 5)]
        public IFormFile Image { get; set; }
    }
}
