namespace MWalks.API.DTOs
{
    public class UpdateWalkDto
    {
        [MaxLength(100, ErrorMessage = "Name maxlength is 100 characters")]
        public string? Name { get; set; }
        [MaxLength(1000, ErrorMessage = "Description maxlength is 100 characters")]
        public string? Description { get; set; }
        [Range(1, 50, ErrorMessage = "LengthInKm has to be between 1 and 50 Km")]
        public double? LengthInKm { get; set; }
        [FileValidator("jpg,jpeg,png", 1)]
        public IFormFile? Image { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? DifficultyId { get; set; }
    }
}
