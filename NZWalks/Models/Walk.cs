namespace MWalks.API.Models
{
    public class Walk : Base
    {
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public byte[]? ImageUrl { get; set; }

        public Guid RegionId { get; set; }
        public Region Region { get; set; }

        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }

    }
}
