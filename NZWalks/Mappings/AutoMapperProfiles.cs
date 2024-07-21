namespace MWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<Walk, WalkDto>()
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => src.Difficulty))
                .ReverseMap();

            CreateMap<CreateWalkDto, Walk>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

            CreateMap<CreateRegionDto, Region>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        }
    }
}
