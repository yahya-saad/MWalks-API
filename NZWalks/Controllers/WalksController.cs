namespace MWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalksController(IWalkRepository _walkRepository, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? ascending,
            int page = 1,
            int limit = 5
            )
        {
            var (data, pagination) = await _walkRepository.GetAllWalksAsync(filterOn, filterQuery, sortBy, ascending ?? true, page, limit);

            var mappedData = _mapper.Map<IEnumerable<WalkDto>>(data);

            var response = new
            {
                Data = mappedData,
                Pagination = pagination
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var existingWalk = await _walkRepository.GetWalkByIdAsync(id);
            var response = _mapper.Map<WalkDto>(existingWalk);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWalkDto dto)
        {

            var walk = await _walkRepository.CreateWalkAsync(dto);
            var response = _mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetById), new { id = walk.Id }, new { message = "Walk Created Successfully", data = response });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDto dto)
        {
            var existingWalk = await _walkRepository.UpdateWalkAsync(id, dto);
            var response = _mapper.Map<WalkDto>(existingWalk);
            return Ok(new { message = "Walk Updated Successfully", data = response });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingRegion = await _walkRepository.DeleteWalkAsync(id);
            return NoContent();
        }

    }
}
