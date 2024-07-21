namespace MWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();
            var response = _mapper.Map<List<RegionDto>>(regions);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);
            var response = _mapper.Map<RegionDto>(region);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromForm] CreateRegionDto dto)
        {
            var newRegion = await _regionRepository.CreataRegionAsync(dto);
            var response = _mapper.Map<RegionDto>(newRegion);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, new { message = "Region Created Successfully", data = response });
        }


        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, UpdateRegionDto dto)
        {
            var existingRegion = await _regionRepository.UpdateRegionAsync(id, dto);
            var response = _mapper.Map<RegionDto>(existingRegion);
            return Ok(new { message = "Region Updated Successfully", data = response });
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingRegion = await _regionRepository.DeleteRegionAsync(id);
            return NoContent();
        }

    }
}
