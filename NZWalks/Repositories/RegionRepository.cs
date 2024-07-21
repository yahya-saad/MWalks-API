namespace MWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RegionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync() => await _context.Regions.ToListAsync();

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
                throw new NotFoundException($"Region with id {id} not found");

            return region;
        }


        public async Task<Region> CreataRegionAsync(CreateRegionDto dto)
        {
            var region = _mapper.Map<Region>(dto);
            region.ImageUrl = dto.Image != null ? await ImageCoverter.ToByteArray(dto.Image) : null;

            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto dto)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
                throw new NotFoundException($"Region with id {id} not found");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                existingRegion.Name = dto.Name;

            if (!string.IsNullOrWhiteSpace(dto.Code))
                existingRegion.Code = dto.Code;

            if (dto.Image != null)
                existingRegion.ImageUrl = await ImageCoverter.ToByteArray(dto.Image);

            await _context.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
                throw new NotFoundException($"Region with id {id} not found");

            _context.Regions.Remove(existingRegion);
            await _context.SaveChangesAsync();
            return existingRegion;
        }


    }
}
