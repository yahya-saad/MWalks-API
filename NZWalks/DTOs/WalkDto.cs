﻿namespace MWalks.API.DTOs
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
        public byte[]? ImageUrl { get; set; }
    }
}
