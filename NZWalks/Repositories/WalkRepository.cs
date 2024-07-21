namespace MWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly PaginationService _paginationService;

        public WalkRepository(AppDbContext context, IMapper logger, PaginationService paginationService)
        {
            _context = context;
            _mapper = logger;
            _paginationService = paginationService;
        }

        public async Task<(IEnumerable<Walk> Data, PaginationMetadata Pagination)> GetAllWalksAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool ascending = true,
            int page = 1,
            int limit = 5)
        {
            var walks = _context.Walks
                .Include(x => x.Region)
                .Include(x => x.Difficulty)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                walks = filterOn.ToLower() switch
                {
                    "name" => walks.Where(x => x.Name.Contains(filterQuery)),
                    "lengthinkm" when double.TryParse(filterQuery, out double lengthInKm) => walks.Where(x => x.LengthInKm == lengthInKm),
                    _ => walks
                };
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                walks = sortBy.ToLower() switch
                {
                    "name" => ascending ?
                        walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name),
                    "lengthinkm" => ascending ?
                        walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm),
                    "region" => ascending ?
                        walks.OrderBy(x => x.Region.Name) : walks.OrderByDescending(x => x.Region.Name),
                    "difficulty" => ascending ?
                        walks.OrderBy(x => x.Difficulty.Name) : walks.OrderByDescending(x => x.Difficulty.Name),
                    _ => walks
                };
            }

            // Pagination
            var (paginatedWalks, pagination) = await _paginationService.GetPaginatedResultAsync(walks, page, limit);

            return (Data: await paginatedWalks.ToListAsync(), Pagination: pagination);
        }





        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk = await _context.Walks
            .Include(x => x.Region)
            .Include(x => x.Difficulty)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
                throw new NotFoundException($"Walk with id ${id} not found");


            return walk;
        }

        public async Task<Walk> CreateWalkAsync(CreateWalkDto dto)
        {

            var region = await _context.Regions.FindAsync(dto.RegionId);
            if (region == null)
                throw new NotFoundException($"Region with id {dto.RegionId} not found.");


            var difficulty = await _context.Difficulties.FindAsync(dto.DifficultyId);
            if (difficulty == null)
                throw new NotFoundException($"Difficulty with id {dto.DifficultyId} not found.");

            var walk = _mapper.Map<Walk>(dto);
            walk.ImageUrl = dto.Image != null ? await ImageCoverter.ToByteArray(dto.Image) : null;
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, UpdateWalkDto dto)
        {
            var existingWalk = await _context.Walks.FindAsync(id);
            if (existingWalk == null)
                throw new NotFoundException($"Walk with id ${id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                existingWalk.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                existingWalk.Description = dto.Description;

            if (dto.LengthInKm.HasValue)
                existingWalk.LengthInKm = dto.LengthInKm.Value;

            if (dto.Image != null)
                existingWalk.ImageUrl = await ImageCoverter.ToByteArray(dto.Image);

            if (dto.RegionId.HasValue)
            {
                var existingRegion = await _context.Regions.FindAsync(dto.RegionId);
                if (existingRegion == null)
                    throw new NotFoundException($"Region with id {dto.RegionId} not found.");

                existingWalk.RegionId = dto.RegionId.Value;
            }

            if (dto.DifficultyId.HasValue)
            {
                var existingDifficulty = await _context.Difficulties.FindAsync(dto.DifficultyId);
                if (existingDifficulty == null)
                    throw new NotFoundException($"Difficulty with id {dto.DifficultyId} not found.");

                existingWalk.DifficultyId = dto.DifficultyId.Value;
            }

            await _context.SaveChangesAsync();
            return existingWalk;

        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
                throw new NotFoundException($"Walk with id ${id} not found");

            _context.Walks.Remove(existingWalk);
            await _context.SaveChangesAsync();
            return existingWalk;
        }
    }
}
