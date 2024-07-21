namespace MWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;


        public LocalImageRepository(IWebHostEnvironment environment, IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _environment = environment;
            _contextAccessor = contextAccessor;
            _context = context;
        }


        public async Task<Image> Upload(UploadImageDto dto)
        {
            var uniqueName = Guid.NewGuid();
            var extention = Path.GetExtension(dto.Image.FileName);
            var filename = $"{uniqueName}{extention}";

            var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads");
            var savePath = Path.Combine(uploadDirectory, filename);

            if (!Directory.Exists(uploadDirectory))
                Directory.CreateDirectory(uploadDirectory);

            using var stream = new FileStream(savePath, FileMode.Create);
            await dto.Image.CopyToAsync(stream);

            var request = _contextAccessor.HttpContext.Request;
            var urlPath = $"{request.Scheme}://{request.Host}{request.PathBase}/uploads/{filename}";

            var image = new Image
            {
                Id = uniqueName,
                Name = filename,
                ContentType = dto.Image.ContentType,
                Extension = extention,
                SizeInBytes = dto.Image.Length,
                Path = urlPath,
            };

            await _context.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }
        public async Task<bool> DeleteImage(Guid id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
                throw new NotFoundException($"(DB) File not found.");

            var uploadDirectory = Path.Combine(_environment.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadDirectory, image.Name);

            if (!File.Exists(filePath))
                throw new NotFoundException($"(FS) File '{image.Name}' not found.");

            // Delete the image file from the file system
            File.Delete(filePath);

            // Remove the image record from the database
            _context.Images.Remove(image);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
