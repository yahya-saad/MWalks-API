namespace MWalks.API.DTOs
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public byte[]? ImageUrl { get; set; }
    }
}
