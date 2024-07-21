namespace MWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<(IEnumerable<Walk> Data, PaginationMetadata Pagination)> GetAllWalksAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool ascending = true,
            int page = 1,
            int limit = 5
            );
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateWalkAsync(CreateWalkDto dto);
        Task<Walk> UpdateWalkAsync(Guid id, UpdateWalkDto dto);
        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
