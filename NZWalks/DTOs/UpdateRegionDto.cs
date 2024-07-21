namespace MWalks.API.DTOs
{
    public class UpdateRegionDto
    {
        [MaxLength(100, ErrorMessage = "Name maxlength is 100 characters")]
        public string? Name { get; set; }
        [MinLength(3, ErrorMessage = "Code minlength is 3 characters")]
        [MaxLength(3, ErrorMessage = "Code maxlength is 3 characters")]
        public string? Code { get; set; }
        [FileValidator("jpg,jpeg,png", 1)]
        public IFormFile? Image { get; set; }
    }
}
