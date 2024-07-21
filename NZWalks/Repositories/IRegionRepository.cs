namespace MWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<Region> CreataRegionAsync(CreateRegionDto dto);
        Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto dto);
        Task<Region> DeleteRegionAsync(Guid id);
    }
}
