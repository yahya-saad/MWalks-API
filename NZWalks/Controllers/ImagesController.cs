namespace MWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Writer")]
    public class ImagesController(IImageRepository _imageRepository) : ControllerBase
    {

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Upload(UploadImageDto dto)
        {
            var res = await _imageRepository.Upload(dto);
            return Ok(res);
        }

        [HttpDelete("[action]/{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {

            var isDeleted = await _imageRepository.DeleteImage(id);
            if (!isDeleted)
                return BadRequest(new { message = "Something went wrong" });

            return NoContent();
        }
    }

}
